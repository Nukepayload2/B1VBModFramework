Imports System.Collections.Concurrent
Imports System.Threading
Imports b1
Imports UnrealEngine.Engine
Imports UnrealEngine.Runtime

Public Class DispatcherTimer
    Private ReadOnly _rawTimer As New Timer(AddressOf RawTimer_Tick)
    Private ReadOnly _dispatcher As Dispatcher
    Event Tick As EventHandler

    Public Property Interval As TimeSpan

    Public ReadOnly Property IsEnabled As Boolean

    Sub New()
        MyClass.New(Dispatcher.GamePlayThread)
    End Sub

    Sub New(dispatcher As Dispatcher)
        _dispatcher = dispatcher
    End Sub

    Public Sub Start()
        If _IsEnabled Then Return
        _IsEnabled = True
        _rawTimer.Change(Interval, Interval)
    End Sub

    Public Sub [Stop]()
        If Not _IsEnabled Then Return
        _IsEnabled = False
        _rawTimer.Change(Timeout.Infinite, Timeout.Infinite)
    End Sub

    Private Sub RawTimer_Tick(state As Object)
        Dim evtDelegate = TickEvent
        If evtDelegate Is Nothing Then Return
        _dispatcher.Invoke(Sub() evtDelegate(Me, EventArgs.Empty))
    End Sub
End Class

Public Class Dispatcher
    Private ReadOnly _invoke As Action(Of FSimpleDelegate)
    Private ReadOnly _beginInvoke As Action(Of FSimpleDelegate)

    Public Sub New(invoke As Action(Of FSimpleDelegate), beginInvoke As Action(Of FSimpleDelegate))
        _invoke = invoke
        _beginInvoke = beginInvoke
    End Sub

    ''' <summary>
    ''' 在游戏线程运行代码。调用次数多了会卡死游戏，改用 <see cref="GamePlayThread"/>
    ''' </summary>
    <Obsolete("调用次数多了会卡死游戏")>
    Public Shared ReadOnly Property GameThread As New Dispatcher(
        AddressOf FThreading.RunOnGameThread,
        AddressOf FThreading.RunOnGameThreadAsync)

    ''' <summary>
    ''' 在游戏能操作角色的时候执行代码。
    ''' </summary>
    Public Shared ReadOnly Property GamePlayThread As New Dispatcher(
        Sub(act) GamePlayDispatcher.Instance?.Invoke(act),
        Sub(act) GamePlayDispatcher.Instance?.BeginInvoke(act))

    ''' <summary>
    ''' 在后台工作线程执行代码。
    ''' </summary>
    Public Shared ReadOnly Property WorkerThread As New Dispatcher(
        Sub(action)
            Dim hWait As New SemaphoreSlim(0, 1)
            ThreadPool.QueueUserWorkItem(
            Sub(unused)
                action()
                hWait.Release()
            End Sub)
            hWait.Wait()
        End Sub,
        Sub(action) ThreadPool.QueueUserWorkItem(Sub(unused) action()))

    Public Sub Invoke(action As FSimpleDelegate)
        _invoke(action)
    End Sub

    Public Sub BeginInvoke(action As FSimpleDelegate)
        _beginInvoke(action)
    End Sub

End Class

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
End Class
