Imports System.Threading
Imports UnrealEngine.Runtime

Public Class B1SynchronizationContext
    Inherits SynchronizationContext

    Public Shared Sub TryInitForGameThread()
        FThreading.RunOnGameThread(
            Sub()
                If Current Is Nothing Then
                    SetSynchronizationContext(New B1SynchronizationContext)
                End If
            End Sub)
    End Sub

    Public Overrides Sub Post(d As SendOrPostCallback, state As Object)
        GamePlayDispatcher.Instance.BeginInvoke(Sub() d(state))
    End Sub

    Public Overrides Sub Send(d As SendOrPostCallback, state As Object)
        GamePlayDispatcher.Instance.Invoke(Sub() d(state))
    End Sub
End Class
