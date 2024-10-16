﻿Imports System.Collections.Concurrent
Imports System.Threading
Imports b1
Imports UnrealEngine.Engine
Imports UnrealEngine.Runtime
''' <summary>
''' 只在游戏没有暂停的时候会执行的 Dispatcher
''' </summary>
Public Class GamePlayDispatcher
    Inherits GameStateSystemBase

    Private Shared _registered As Boolean
    Private Shared ReadOnly _regTimer As New Timer(AddressOf RepeatRegister_Tick)
    ''' <summary>
    ''' 共享实例，不为空，但是随着游戏读盘会重新注册，存在空档期
    ''' </summary>
    Public Shared ReadOnly Property Instance As New GamePlayDispatcher(True)
    Public ReadOnly Property IsEnabled As Boolean

    Private Shared Sub RepeatRegister_Tick(state As Object)
        TryRegister()
        If Volatile.Read(_registered) Then
            _regTimer.Change(Timeout.Infinite, Timeout.Infinite)
        End If
    End Sub

    Public Shared Sub Register()
        TryRegister()

        If Not Volatile.Read(_registered) Then
            _regTimer.Change(100, 100)
        End If
    End Sub

    Private Shared Sub TryRegister()
        If Volatile.Read(_registered) Then Return
        Dim stateRaw = ActiveWorld.GetGameState
        If stateRaw Is Nothing Then
            Return
        End If
        Dim state = TryCast(stateRaw, BGGGameStateCS)
        Dim compMgr = state?.ActorCompContainerCS
        If compMgr Is Nothing OrElse compMgr.IsDestroyed OrElse compMgr.IsBeingDestroyed Then
            Return
        End If
        If _isReregistering AndAlso compMgr Is _lastCompMgr Then
            PrintLine("CompMgr disposing")
            Return
        End If
        Volatile.Write(_registered, True)
        _lastCompMgr = compMgr
        FThreading.RunOnGameThread(Sub() compMgr.AddComp(Instance))
        PrintLine("Registered component")
        _isReregistering = False
    End Sub

    Private Shared _messageId As Long
    Private Shared _lastCompMgr As UActorCompContainerCS
    Private Shared _isReregistering As Boolean

    Private Shared Sub PrintLine(text As String)
        Console.WriteLine($"GameThreadMessageBump {Interlocked.Increment(_messageId)}: {text}")
    End Sub

    Sub New()
        MyClass.New(False)
    End Sub

    Sub New(isModInstance As Boolean)
        If Not isModInstance Then Return
        _messages = New ConcurrentQueue(Of FSimpleDelegate)
    End Sub

    Public Overrides Function GetTickGroupMask() As Integer
        Return 1024
    End Function

    Public Overrides Sub OnAttach()
        MyBase.OnAttach()
        SetCanTick(False)
        PrintLine("OnAttach")
    End Sub

    Public Overrides Sub OnBeginPlay()
        MyBase.OnBeginPlay()
        SetCanTick(True)
        PrintLine("OnBeginPlay")
        _IsEnabled = True
    End Sub

    Public Overrides Sub OnEndPlay(EndPlayReason As EEndPlayReason)
        MyBase.OnEndPlay(EndPlayReason)
        PrintLine($"OnEndPlay {EndPlayReason}. Reregistering...")
        Volatile.Write(_registered, False)
        _regTimer.Change(100, 100)
        _isReregistering = True
    End Sub

    Private ReadOnly _messages As ConcurrentQueue(Of FSimpleDelegate)

    Public Overrides Sub OnTickWithGroup(DeltaTime As Single, TickGroup As Integer)
        MyBase.OnTickWithGroup(DeltaTime, TickGroup)
        DoEvents()
    End Sub

    Private Sub ThrowForMessagesUnavailable()
        If _messages Is Nothing Then Throw New InvalidOperationException("This dispatcher has shut down or not loaded.")
    End Sub

    Private Sub DoEvents()
        ThrowForMessagesUnavailable()
        Dim curMessage As FSimpleDelegate = Nothing
        Do While _messages.TryDequeue(curMessage)
            curMessage()
        Loop
    End Sub

    Public Sub Invoke(act As FSimpleDelegate)
        If FThreading.IsInGameThread Then
            act()
            Return
        End If
        ThrowForMessagesUnavailable()
        Dim hWait As New SemaphoreSlim(0, 1)
        _messages.Enqueue(
            Sub()
                act()
                hWait.Release()
            End Sub)
        hWait.Wait()
    End Sub

    Public Async Function InvokeAsync(act As FSimpleDelegate) As Task
        ThrowForMessagesUnavailable()
        Dim tcs As New TaskCompletionSource(Of Object)
        _messages.Enqueue(
            Sub()
                act()
                tcs.SetResult(Nothing)
            End Sub)
        Await tcs.Task
    End Function

    Public Sub BeginInvoke(act As FSimpleDelegate)
        ThrowForMessagesUnavailable()
        _messages.Enqueue(act)
    End Sub

    Public Function CheckAccess() As Boolean
        Return FThreading.IsInGameThread
    End Function
End Class
