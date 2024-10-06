Imports b1
Imports b1.BGW
Imports UnrealEngine.Engine
Imports UnrealEngine.Runtime

Namespace MyService

    Public Class WindowProxy
        ''' <summary>
        ''' 用于在游戏窗口中显示一个菜单
        ''' </summary>
        Public ReadOnly Property ContextMenu As New MenuFlyout

    End Class

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
    End Class

    Public Class WorldProxy

        Public ReadOnly Property Instance As UWorld
            Get
                Dim uobjectRef As UObjectRef = GCHelper.FindRef(FGlobals.GWorld)
                Return TryCast(uobjectRef?.Managed, UWorld)
            End Get
        End Property

        Public Function LoadAsset(Of T As UObject)(asset As String) As T
            Return BGW_PreloadAssetMgr.Get(Instance).TryGetCachedResourceObj(Of T)(
            asset, ELoadResourceType.SyncLoadAndCache, EAssetPriority.Default,
            Nothing, -1, -1)
        End Function

    End Class

    Public Class PlayerProxy
        Public ReadOnly Property Controller As BGP_PlayerControllerB1
            Get
                Dim w = My.World.Instance
                If w Is Nothing Then Return Nothing
                Return TryCast(UGSE_EngineFuncLib.GetFirstLocalPlayerController(w), BGP_PlayerControllerB1)
            End Get
        End Property

        Public ReadOnly Property Pawn As BGUPlayerCharacterCS
            Get
                Dim c = Controller
                If c Is Nothing Then Return Nothing
                Return TryCast(c.GetControlledPawn, BGUPlayerCharacterCS)
            End Get
        End Property

        Public ReadOnly Property EventCollection As BUS_GSEventCollection
            Get
                Dim p = Pawn
                If p Is Nothing Then Return Nothing
                Return BUS_EventCollectionCS.Get(p)
            End Get
        End Property

        Public ReadOnly Property Status As PlayerStatus
            Get
                Dim p = Pawn
                If p Is Nothing Then Return Nothing
                Return New PlayerStatus(p)
            End Get
        End Property
    End Class

End Namespace