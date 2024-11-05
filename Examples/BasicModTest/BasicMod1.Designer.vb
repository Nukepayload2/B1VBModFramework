Imports System.Runtime.ExceptionServices
Imports Nukepayload2.GameMods.BlackMythWukong.Logging

Partial Class BasicMod1
    Inherits ModBase

    Shared Sub New()
        ' 有时候因为异常被处理了而看不到错误消息
        AddHandler AppDomain.CurrentDomain.FirstChanceException, AddressOf OnFirstChangeException
    End Sub

    Private Shared Sub OnFirstChangeException(sender As Object, e As FirstChanceExceptionEventArgs)
        Console.WriteLine($"First chance exception: {e.Exception}")
    End Sub

    Sub New()
        My.Mod = Me
        InitializeComponents()
    End Sub

    Partial Private Sub InitializeComponents()
        ' 在另一个文件里面，用于初始化组件。这个过程可以是源生成器写的，也可以是人写的。
    End Sub

    Protected Overrides Sub OnLoad()
        With My.Log.TraceSource.Listeners
            .Clear()
            .Add(New FileLogTraceListener With {.Location = LogFileLocation.ModDirectory})
            .Add(New ConsoleTraceListener)
        End With
        GamePlayDispatcher.Register()
        B1SynchronizationContext.TryInitForGameThread()
        My.Computer.InputManager.StartListening()
        MyBase.OnLoad()
    End Sub

    Protected Overrides Sub OnUnload()
        MyBase.OnUnload()
        My.Computer.InputManager.StopListening()
        My.Log.TraceSource.Close()
    End Sub

End Class
