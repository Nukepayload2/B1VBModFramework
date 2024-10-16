Imports System.Runtime.CompilerServices
Imports b1
Imports UnrealEngine.InputCore

Public Interface IMouseDevice
    ReadOnly Property IsKeyDown(key As EKeys) As Boolean
    Default ReadOnly Property Buttons(key As EKeys) As InputManager.KeyOrButton
End Interface

Public Interface IKeyboardDevice
    ReadOnly Property IsKeyDown(key As EKeys) As Boolean
    Default ReadOnly Property Keys(key As EKeys) As InputManager.KeyOrButton
End Interface

Public Interface IGamepadDevice
    ReadOnly Property IsKeyDown(key As EKeys) As Boolean
    Default ReadOnly Property Buttons(key As EKeys) As InputManager.KeyOrButton
End Interface

Public Class InputManager
    Implements IMouseDevice, IKeyboardDevice, IGamepadDevice

    WithEvents PlayerKeyStateTimer As New DispatcherTimer(Dispatcher.WorkerThread) With {
        .Interval = TimeSpan.FromMilliseconds(10)
    }

    Default Public ReadOnly Property Item(key As EKeys) As KeyOrButton Implements IMouseDevice.Buttons, IKeyboardDevice.Keys, IGamepadDevice.Buttons
        Get
            Return New KeyOrButton(key, Me)
        End Get
    End Property

    Private ReadOnly _keyDownListens As New Dictionary(Of EKeys, EventHandler(Of KeyEventArgs))
    Private ReadOnly _keyUpListens As New Dictionary(Of EKeys, EventHandler(Of KeyEventArgs))
    Private ReadOnly _keyStateListens As New Dictionary(Of EKeys, KeyState)

    Public Sub StartListening()
        PlayerKeyStateTimer.Start()
    End Sub

    Public Sub StopListening()
        PlayerKeyStateTimer.Stop()
    End Sub

    Public ReadOnly Property IsKeyDown(key As EKeys) As Boolean Implements IMouseDevice.IsKeyDown, IKeyboardDevice.IsKeyDown, IGamepadDevice.IsKeyDown
        Get
            Dim playerCtl = My.Player.Controller
            Return If(playerCtl?.IsInputKeyDown(New FKey(key)), False)
        End Get
    End Property

    Private Sub PlayerKeyStateTimer_Tick() Handles PlayerKeyStateTimer.Tick
        If _keyStateListens.Count = 0 Then
            Return
        End If
        Dim world = ActiveWorld
        If world Is Nothing Then
            Return
        End If
        Dim playerCtl = TryCast(UGSE_EngineFuncLib.GetFirstLocalPlayerController(world), BGP_PlayerControllerB1)
        If playerCtl?.GetControlledPawn Is Nothing Then
            Return
        End If

        Dim disp = GamePlayDispatcher.Instance
        If Not disp.IsEnabled Then Return

        Dim ctrlDown = playerCtl.IsInputKeyDown(New FKey(EKeys.LeftControl)) OrElse playerCtl.IsInputKeyDown(New FKey(EKeys.RightControl))
        Dim altDown = playerCtl.IsInputKeyDown(New FKey(EKeys.LeftAlt)) OrElse playerCtl.IsInputKeyDown(New FKey(EKeys.RightAlt))
        Dim shiftDown = playerCtl.IsInputKeyDown(New FKey(EKeys.LeftShift)) OrElse playerCtl.IsInputKeyDown(New FKey(EKeys.RightShift))
        Dim winDown = playerCtl.IsInputKeyDown(New FKey(EKeys.LeftCommand)) OrElse playerCtl.IsInputKeyDown(New FKey(EKeys.RightCommand))

        Dim modifier = ModifierKeys.None
        If ctrlDown Then
            modifier.AddFlag(ModifierKeys.Control)
        End If
        If altDown Then
            modifier.AddFlag(ModifierKeys.Alt)
        End If
        If shiftDown Then
            modifier.AddFlag(ModifierKeys.Shift)
        End If
        If winDown Then
            modifier.AddFlag(ModifierKeys.Windows)
        End If

        For Each state In _keyStateListens.Values
            Dim key = state.Key
            Dim isPressedNow = playerCtl.IsInputKeyDown(New FKey(key))
            Dim oldPressed = state.Pressed
            state.Pressed = isPressedNow
            Dim keyDownHandler As EventHandler(Of KeyEventArgs) = Nothing
            _keyDownListens.TryGetValue(key, keyDownHandler)

            Dim keyUpHandler As EventHandler(Of KeyEventArgs) = Nothing
            _keyUpListens.TryGetValue(key, keyUpHandler)

            If keyDownHandler IsNot Nothing AndAlso Not oldPressed AndAlso isPressedNow Then
                disp.BeginInvoke(Sub() keyDownHandler(Me, New KeyEventArgs(key, modifier, world, playerCtl)))
            End If

            If keyUpHandler IsNot Nothing AndAlso oldPressed AndAlso Not isPressedNow Then
                disp.BeginInvoke(Sub() keyUpHandler(Me, New KeyEventArgs(key, modifier, world, playerCtl)))
            End If
        Next
    End Sub

    Private Sub AddKeyState(key As EKeys)
        Dim state As KeyState = Nothing
        If Not _keyStateListens.TryGetValue(key, state) Then
            state = New KeyState With {.Key = key}
            _keyStateListens.Add(key, state)
        End If
        state.RefCount += 1
    End Sub

    Private Sub RemoveKeyState(key As EKeys)
        Dim state As KeyState = Nothing
        If _keyStateListens.TryGetValue(key, state) Then
            state.RefCount -= 1
            If state.RefCount = 0 Then
                _keyStateListens.Remove(key)
            End If
        End If
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
    Private Sub [AddHandler](key As EKeys, value As EventHandler(Of KeyEventArgs), eventDict As Dictionary(Of EKeys, EventHandler(Of KeyEventArgs)))
        AddKeyState(key)
        Dim existinghandler As EventHandler(Of KeyEventArgs) = Nothing
        eventDict.TryGetValue(key, existinghandler)
        eventDict(key) = [Delegate].Combine(existinghandler, value)
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
    Private Sub [RemoveHandler](key As EKeys, value As EventHandler(Of KeyEventArgs), eventDict As Dictionary(Of EKeys, EventHandler(Of KeyEventArgs)))
        RemoveKeyState(key)
        Dim existinghandler As EventHandler(Of KeyEventArgs) = Nothing
        If eventDict.TryGetValue(key, existinghandler) Then
            Dim removed = [Delegate].Remove(existinghandler, value)
            If removed Is Nothing Then
                eventDict.Remove(key)
            Else
                eventDict(key) = removed
            End If
        End If
    End Sub

    Private Class KeyState
        Public Property Key As EKeys
        Public Property Pressed As Boolean
        Public Property RefCount As Integer
    End Class

    Public Class KeyOrButton
        Private ReadOnly _key As EKeys
        Private ReadOnly _parent As InputManager

        Sub New(key As EKeys, parent As InputManager)
            _key = key
            _parent = parent
        End Sub

        Public ReadOnly Property IsPressed As Boolean
            Get
                Return _parent.IsKeyDown(_key)
            End Get
        End Property

        Custom Event KeyDown As EventHandler(Of KeyEventArgs)
            AddHandler(value As EventHandler(Of KeyEventArgs))
                _parent.AddHandler(_key, value, _parent._keyDownListens)
            End AddHandler
            RemoveHandler(value As EventHandler(Of KeyEventArgs))
                _parent.RemoveHandler(_key, value, _parent._keyDownListens)
            End RemoveHandler
            RaiseEvent(sender As Object, e As KeyEventArgs)
                ' 外面处理这个情况
            End RaiseEvent
        End Event

        Custom Event KeyUp As EventHandler(Of KeyEventArgs)
            AddHandler(value As EventHandler(Of KeyEventArgs))
                _parent.AddHandler(_key, value, _parent._keyUpListens)
            End AddHandler
            RemoveHandler(value As EventHandler(Of KeyEventArgs))
                _parent.RemoveHandler(_key, value, _parent._keyUpListens)
            End RemoveHandler
            RaiseEvent(sender As Object, e As KeyEventArgs)
                ' 外面处理这个情况
            End RaiseEvent
        End Event
    End Class

End Class
