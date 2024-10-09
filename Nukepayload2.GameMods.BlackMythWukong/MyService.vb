Imports b1
Imports b1.BGW
Imports b1.Plugins.AkAudio
Imports B1UI.GSUI
Imports Nukepayload2.GameMods.BlackMythWukong.Media
Imports UnrealEngine.Engine
Imports UnrealEngine.Runtime

Namespace MyService

    Public Class WindowProxy
        ''' <summary>
        ''' 用于在游戏窗口中显示一个菜单
        ''' </summary>
        Public ReadOnly Property ContextMenu As New MenuFlyout

        Public ReadOnly Property CanEnterTakePhotoMode As Boolean
            Get
                Return GSB1UIUtil.GetIsCanEnterTakePhotoMode(My.World.Instance, True)
            End Get
        End Property

        Public Sub NavigateTo(pageName As String)
            pageName.RequireNotNull(NameOf(pageName))

            UGameplayStatics.OpenLevel(My.World.Instance, New FName(pageName))
        End Sub
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

        Public ReadOnly Property Audio As New AudioProxy
    End Class

    Public Class AudioProxy
        ''' <summary>
        ''' 播放指定的系统声音。
        ''' </summary>
        ''' <param name="systemSoundName">自定义的系统声音名称</param>
        Public Shared Sub PlaySystemSound(systemSoundName As String)
            B1UI.Script.GSUI.Util.GSUIAudioUtil.PlayUISound(systemSoundName)
        End Sub

        ''' <summary>
        ''' 播放指定的系统声音。
        ''' </summary>
        ''' <param name="systemSound">预设的系统声音</param>
        Public Shared Sub PlaySystemSound(systemSound As SystemSound)
            B1UI.Script.GSUI.Util.GSUIAudioUtil.PlayUISound(systemSound.ToString)
        End Sub

        ''' <summary>
        ''' 播放指定资源的声音。
        ''' </summary>
        ''' <param name="resourceUri">
        ''' 例如：/Game/00Main/Audio/SFX/UI/HUD/EVT_ui_hud_hint_itembig_disappear.EVT_ui_hud_hint_itembig_disappear
        ''' </param>
        Public Shared Sub PlayBank(resourceUri As String)
            Dim bankName = $"AkAudioEvent'{resourceUri}'"
            B1UI.Script.GSUI.Util.GSUIAudioUtil.PlayUISoundWithAkEvent(
                B1UI.Script.GSUI.Util.GSUIAudioUtil.LoadBank(bankName),
                New List(Of EAkCallbackType), Nothing)
        End Sub
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
