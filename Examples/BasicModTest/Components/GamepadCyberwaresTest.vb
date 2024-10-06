Imports b1
Imports b1.EventDelDefine
Imports BtlShare
Imports UnrealEngine.Engine
Imports UnrealEngine.InputCore

' 这是个秘技，在玩家触发特定条件（左右摇杆输入）后可以激活并持续一段时间。将逐个开启赛博植入体。开启的层数越多，玩家的战斗力越强。
Class GamepadCyberwaresTest
    Inherits ModComponentBase

    WithEvents CyberwareLevelTimer As New DispatcherTimer

    Private Sub GamepadCyberwaresTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        Console.WriteLine("Press left+right thumb sticks to activate cyberwares")
        LeftStick = My.Computer.Gamepad.Buttons(EKeys.Gamepad_LeftThumbstick)
        RightStick = My.Computer.Gamepad.Buttons(EKeys.Gamepad_RightThumbstick)
    End Sub

    Private Class CyberwareContext
        Sub New(player As APlayerController, actor As APawn)
            PlayerController = player
            Pawn = actor
            Status = actor.GetStatus
        End Sub

        Public ReadOnly Property Pawn As APawn
        Public ReadOnly Property PlayerController As APlayerController
        Public ReadOnly Property Status As PlayerStatus
    End Class

    Private Interface ICyberware
        ReadOnly Property EnterMessage As String
        ReadOnly Property ExitMessage As String
        ReadOnly Property Duration As TimeSpan
        Sub Activate(context As CyberwareContext)
        Sub Deactivate(context As CyberwareContext)
    End Interface

    ''' <summary>
    ''' 活血泵，恢复状态
    ''' </summary>
    Private Class Cyberware1活血泵
        Implements ICyberware

        Public Overridable ReadOnly Property EnterMessage As String Implements ICyberware.EnterMessage
            Get
                Return "秘技一，活血泵，恢复状态！"
            End Get
        End Property

        Public ReadOnly Property ExitMessage As String Implements ICyberware.ExitMessage
            Get
                Return "活血泵可以再次使用了。"
            End Get
        End Property

        Public ReadOnly Property Duration As TimeSpan Implements ICyberware.Duration
            Get
                Return TimeSpan.FromSeconds(10)
            End Get
        End Property

        Public Overridable Sub Activate(context As CyberwareContext) Implements ICyberware.Activate
            Dim status = context.Status
            ' 恢复状态
            status.Value(EBGUAttrFloat.Hp) = status.Value(EBGUAttrFloat.HpMax)
        End Sub

        Public Overridable Sub Deactivate(context As CyberwareContext) Implements ICyberware.Deactivate
            ' 没什么要做的，因为上面这些值都是一次性改变
        End Sub
    End Class

    ''' <summary>
    ''' 狂暴，霸体并且输出更猛烈
    ''' </summary>
    Private Class Cyberware2狂暴
        Implements ICyberware

        Public ReadOnly Property EnterMessage As String Implements ICyberware.EnterMessage
            Get
                Return "叠加两层秘技，狂暴，攻击更加猛烈！"
            End Get
        End Property

        Public ReadOnly Property ExitMessage As String Implements ICyberware.ExitMessage
            Get
                Return "狂暴的效果结束了"
            End Get
        End Property

        Public ReadOnly Property Duration As TimeSpan Implements ICyberware.Duration
            Get
                Return TimeSpan.FromSeconds(8)
            End Get
        End Property

        Private _oldTenacityBase As Single

        Public Sub Activate(context As CyberwareContext) Implements ICyberware.Activate
            Dim status = context.Status
            ' 备份
            _oldTenacityBase = status.Value(EBGUAttrFloat.TenacityBase)

            ' 霸体，恢复耐力
            status.Value(EBGUAttrFloat.TenacityBase) = 10000
            status.Value(EBGUAttrFloat.Stamina) = status.Value(EBGUAttrFloat.StaminaMax)

            ' 把棍势加满，为输出做准备
            status.Value(EBGUAttrFloat.Pevalue) = status.Value(EBGUAttrFloat.PevalueMax)

            ' 攻击力上升
            status.Value(EBGUAttrFloat.AtkBase) *= 1.25F

            ' 锁耐力
            status.IsSimpleStateEnabled(EBGUSimpleState.StaminaLock) = True

            ' 锁豆过于变态了，先去掉了
            'status.IsSimpleStateEnabled(EBGUSimpleState.PELock) = True
        End Sub

        Public Sub Deactivate(context As CyberwareContext) Implements ICyberware.Deactivate
            Dim status = context.Status
            ' 还原备份的数值
            status.Value(EBGUAttrFloat.TenacityBase) = _oldTenacityBase

            ' 还原攻击力
            status.Value(EBGUAttrFloat.AtkBase) /= 1.25F

            ' 取消锁耐力
            status.IsSimpleStateEnabled(EBGUSimpleState.StaminaLock) = False

            ' 锁豆过于变态了，先去掉了
            'status.IsSimpleStateEnabled(EBGUSimpleState.PELock) = False
        End Sub
    End Class

    ''' <summary>
    ''' 克伦齐科夫，拥有伤害减免和时间放缓的效果
    ''' </summary>
    Private Class Cyberware3克伦齐科夫
        Implements ICyberware, IDisposable

        Public ReadOnly Property EnterMessage As String Implements ICyberware.EnterMessage
            Get
                Return "叠加三层秘技，克伦齐科夫，翻滚或闪身时启动！"
            End Get
        End Property

        Public ReadOnly Property ExitMessage As String Implements ICyberware.ExitMessage
            Get
                Return "克伦齐科夫正在关闭"
            End Get
        End Property

        Public ReadOnly Property Duration As TimeSpan Implements ICyberware.Duration
            Get
                ' 这个效果是闪避触发的，在规定时间内做出动作就可以暂时启动
                Return TimeSpan.FromSeconds(60)
            End Get
        End Property

        Private _oldDmgDef, _oldDmgDefBase, _oldCritRateBase As Single
        Private _lastStatus As PlayerStatus

        ' DispatcherTimer 用于控制效果时间不是最优做法，因为暂停界面还在计时，会出现早期版本小骊龙的同款 bug。这里只是做个简单演示，不考虑细节问题。
        WithEvents EffectEndTimer As New DispatcherTimer With {.Interval = TimeSpan.FromSeconds(3.75)}
        WithEvents ProtectionEffectEndTimer As New DispatcherTimer With {.Interval = TimeSpan.FromSeconds(4)}

        Private Enum KerenzikovStatus
            Ready
            Activated
            ProtectionActivated
        End Enum

        Private _kerenzikovStatus As KerenzikovStatus

        Public Sub Activate(context As CyberwareContext) Implements ICyberware.Activate
            Dim status = context.Status
            ' 备份
            _lastStatus = status

            Dim events = BUS_EventCollectionCS.Get(context.Pawn)
            If events IsNot Nothing Then
                events.Evt_BeginPreciseDodge += New Del_TriggerRollSkill(AddressOf OnDodge)
            End If
        End Sub

        Private Sub OnDodge(rollDir As ESkillDirection)
            If _kerenzikovStatus <> KerenzikovStatus.Ready Then Return
            _kerenzikovStatus = KerenzikovStatus.Activated

            ' 增加暴击率，时间放缓的时候更容易命中要害
            Dim status = _lastStatus
            _oldCritRateBase = status.Value(EBGUAttrFloat.CritRateBase)
            status.Value(EBGUAttrFloat.CritRateBase) = 10000

            ' 开始慢动作，等待一定时间之后结束这个慢动作
            ActiveWorld.SetGlobalTimeDilation(0.4)
            EffectEndTimer.Start()

            ShowTip("克伦齐科夫已激活，反应敏捷，直击要害！")
        End Sub

        Private Sub EndSlowEffect() Handles EffectEndTimer.Tick
            If _kerenzikovStatus <> KerenzikovStatus.Activated Then Return
            _kerenzikovStatus = KerenzikovStatus.ProtectionActivated

            EffectEndTimer.Stop()

            Dim status = _lastStatus

            ' 克伦齐科夫回护：减伤
            _oldDmgDef = status.Value(EBGUAttrFloat.DmgDef)
            _oldDmgDefBase = status.Value(EBGUAttrFloat.DmgDefBase)

            status.Value(EBGUAttrFloat.DmgDef) = 8000
            status.Value(EBGUAttrFloat.DmgDefBase) = 8000

            ' 还原暴击率
            status.Value(EBGUAttrFloat.CritRateBase) = _oldCritRateBase

            ' 一段时间后把减伤去了
            ProtectionEffectEndTimer.Start()

            ' 结束慢动作
            ActiveWorld.SetGlobalTimeDilation(1)

            ShowTip("处于克伦齐科夫回护中，受伤害减少")
        End Sub

        Private Sub EndProtectionEffect() Handles ProtectionEffectEndTimer.Tick
            If _kerenzikovStatus <> KerenzikovStatus.ProtectionActivated Then Return
            _kerenzikovStatus = KerenzikovStatus.Ready

            ProtectionEffectEndTimer.Stop()

            Dim status = _lastStatus

            ' 还原减伤
            status.Value(EBGUAttrFloat.DmgDef) = _oldDmgDef
            status.Value(EBGUAttrFloat.DmgDefBase) = _oldDmgDefBase

            ShowTip("克伦齐科夫回护结束")
        End Sub

        Public Sub Deactivate(context As CyberwareContext) Implements ICyberware.Deactivate
            ' 只把触发条件干掉就行了
            Dim events = BUS_EventCollectionCS.Get(context.Pawn)
            If events IsNot Nothing Then
                events.Evt_BeginPreciseDodge -= New Del_TriggerRollSkill(AddressOf OnDodge)
            End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' 换场景了，重置
            ProtectionEffectEndTimer.Stop()
            EffectEndTimer.Stop()
            _lastStatus = Nothing
            _kerenzikovStatus = KerenzikovStatus.Ready
        End Sub
    End Class

    ''' <summary>
    ''' 量子调谐，允许你疯狂放技能
    ''' </summary>
    Private Class Cyberware4量子调谐
        Implements ICyberware

        Private _oldEnergyInc, _oldEnergyIncBase As Single

        Private ReadOnly Property EnterMessage As String Implements ICyberware.EnterMessage
            Get
                Return "叠加四层秘技，量子调谐，技能冷却重置！"
            End Get
        End Property

        Public ReadOnly Property ExitMessage As String Implements ICyberware.ExitMessage
            Get
                Return "量子调谐能够再次使用了"
            End Get
        End Property

        Public ReadOnly Property Duration As TimeSpan Implements ICyberware.Duration
            Get
                Return TimeSpan.FromSeconds(5)
            End Get
        End Property

        Public Sub Activate(context As CyberwareContext) Implements ICyberware.Activate
            Dim status = context.Status
            ' 备份
            _oldEnergyInc = status.Value(EBGUAttrFloat.EnergyIncreaseSpeed)
            _oldEnergyIncBase = status.Value(EBGUAttrFloat.EnergyIncreaseSpeedBase)
            ' 恢复 MP
            status.Value(EBGUAttrFloat.Mp) = status.Value(EBGUAttrFloat.MpMax)
            ' 变身冷却的速度
            status.Value(EBGUAttrFloat.EnergyIncreaseSpeed) = 10000
            status.Value(EBGUAttrFloat.EnergyIncreaseSpeedBase) = 10000
            ' 冷却值
            status.Value(EBGUAttrFloat.SpecialEnergy) = status.Value(EBGUAttrFloat.SpecialEnergyMax)
            status.Value(EBGUAttrFloat.FabaoEnergy) = status.Value(EBGUAttrFloat.FabaoEnergyMax)
            status.Value(EBGUAttrFloat.VigorEnergy) = status.Value(EBGUAttrFloat.VigorEnergyMax)
            ' 技能快速冷却
            status.AddBuff(BuffIds.FastCoolDown, TimeSpan.FromSeconds(1))
        End Sub

        Public Sub Deactivate(context As CyberwareContext) Implements ICyberware.Deactivate
            Dim status = context.Status
            ' 还原备份的数值
            status.Value(EBGUAttrFloat.EnergyIncreaseSpeed) = _oldEnergyInc
            status.Value(EBGUAttrFloat.EnergyIncreaseSpeedBase) = _oldEnergyIncBase
        End Sub
    End Class

    Private _cyberwareStartActor As AActor
    Private ReadOnly _activeCyberwares As New Stack(Of ICyberware)

    Private ReadOnly _cyberwares As ICyberware() = {
        New Cyberware1活血泵, New Cyberware2狂暴, New Cyberware3克伦齐科夫, New Cyberware4量子调谐
    }

    Private Sub CyberwareWhenLeftRightStickPressed(e As KeyEventArgs)
        Dim player = e.Player
        Dim actor = player.GetControlledPawn
        If actor Is Nothing Then
            ShowTip("当前状态无法使用秘技")
            Return
        End If
        _cyberwareStartActor = actor

        Dim curCyberware As ICyberware
        Select Case _activeCyberwares.Count
            Case 0 To 3
                curCyberware = _cyberwares(_activeCyberwares.Count)
            Case Else
                ShowTip("秘技最多只能叠四层")
                Return
        End Select

        ShowTip(curCyberware.EnterMessage)
        curCyberware.Activate(New CyberwareContext(player, actor))
        _activeCyberwares.Push(curCyberware)
        CyberwareLevelTimer.Interval = curCyberware.Duration
        CyberwareLevelTimer.Stop()
        CyberwareLevelTimer.Start()
    End Sub

    Private Sub CyberwareLevelTimer_Tick(sender As Object, e As EventArgs) Handles CyberwareLevelTimer.Tick
        Dim controller = My.Player.Controller
        Dim actor = controller?.GetControlledPawn

        If actor Is Nothing OrElse _cyberwareStartActor IsNot actor OrElse actor.IsDestroyed OrElse actor.IsActorBeingDestroyed Then
            ' 读档或者换大地图了
            For Each cy In _activeCyberwares.OfType(Of IDisposable)
                cy.Dispose()
            Next
            _activeCyberwares.Clear()
            CyberwareLevelTimer.Stop()
            Return
        End If

        If _activeCyberwares.Count > 0 Then
            ' 退出当前层的义体
            Dim lastCyberware = _activeCyberwares.Pop
            ShowTip(lastCyberware.ExitMessage)
            lastCyberware.Deactivate(New CyberwareContext(controller, actor))

            ' 准备退出下一层的义体
            CyberwareLevelTimer.Stop()
            If _activeCyberwares.Count > 0 Then
                Dim nextCyberware = _activeCyberwares.Peek
                CyberwareLevelTimer.Interval = nextCyberware.Duration
                CyberwareLevelTimer.Start()
            End If
        End If
    End Sub

    ' 判定左右摇杆都按下了

    WithEvents LeftStick As InputManager.KeyOrButton
    WithEvents RightStick As InputManager.KeyOrButton

    Private _leftStickPressed As Boolean
    Private _rightStickPressed As Boolean
    Private _tipShown As Boolean

    Private Sub LeftStick_KeyDown(sender As Object, e As KeyEventArgs) Handles LeftStick.KeyDown
        _leftStickPressed = True
        If _rightStickPressed Then
            CyberwareWhenLeftRightStickPressed(e)
        ElseIf Not _tipShown Then
            ShowTip("你按下了左摇杆，同时按右摇杆使用秘技")
            _tipShown = True
        End If
    End Sub

    Private Sub LeftStick_KeyUp(sender As Object, e As KeyEventArgs) Handles LeftStick.KeyUp
        _leftStickPressed = False
        Console.WriteLine("Left thumb stick up")
    End Sub

    Private Sub RightStick_KeyDown(sender As Object, e As KeyEventArgs) Handles RightStick.KeyDown
        _rightStickPressed = True
        If _leftStickPressed Then
            CyberwareWhenLeftRightStickPressed(e)
        ElseIf Not _tipShown Then
            ShowTip("你按下了右摇杆，同时按左摇杆使用秘技")
            _tipShown = True
        End If
    End Sub

    Private Sub RightStick_KeyUp(sender As Object, e As KeyEventArgs) Handles RightStick.KeyUp
        _rightStickPressed = False
        Console.WriteLine("Right thumb stick up")
    End Sub

End Class

' 原本的设想：
' 纳米镀层 弹刀，但是5秒内弹了超过3次会CD BGUFunctionLibraryCS.BGUSetUnitSimpleState EBGUSimpleState.BounceAttack
' 量子调谐：立即重置 CD
' 活血泵：回血，回耐力
' 狂暴：加攻击，加霸体，加暴击，加耐力，让耐力用不完，加棍势，加速度。可惜速度加了动作会乱套。
' 克伦齐科夫：翻滚或者闪身会进入慢动作
' 克伦齐科夫增幅：闪身增加护盾和减伤
' 克伦齐科夫回护：克伦齐科夫结束后减伤
' 斯安威斯坦 给自己加速度的同时给世界相同的减速度。可惜动了角色速度之后动作会乱套。
' 生物监测：发现 HP 低下的时候，自动治疗。有 CD。
' 行为特征面板：赋予新的变身能力
