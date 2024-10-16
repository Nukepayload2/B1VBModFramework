Imports System.Threading
Imports UnrealEngine.Runtime

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
