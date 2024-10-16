Namespace MyService
    Public Class ComputerProxy

        ''' <summary>
        ''' 当前的按键输入服务，可以用来监听按键状态的变化
        ''' </summary>
        Public ReadOnly Property InputManager As New InputManager

        ' 由于 KeyEventManager 处理得了所有按键状态，下面的属性只是个别名。

        ''' <summary>
        ''' 获取键盘的状态
        ''' </summary>
        Public ReadOnly Property Keyboard As IKeyboardDevice
            Get
                Return InputManager
            End Get
        End Property

        ''' <summary>
        ''' 获取手柄的状态
        ''' </summary>
        Public ReadOnly Property Gamepad As IGamepadDevice
            Get
                Return InputManager
            End Get
        End Property

        ''' <summary>
        ''' 获取鼠标的状态
        ''' </summary>
        Public ReadOnly Property Mouse As IMouseDevice
            Get
                Return InputManager
            End Get
        End Property

        Public ReadOnly Property Audio As New AudioProxy
    End Class

End Namespace
