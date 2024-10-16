Imports System.Threading

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
