Imports BtlShare
Imports UnrealEngine.InputCore

Class StatusDisplay
    Inherits ModComponentBase

    Private Sub StatusDisplay_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler My.Computer.Keyboard.Keys(EKeys.I).KeyDown, AddressOf OnIKeyDown
        Console.WriteLine("Press Ctrl+I to show player status.")
    End Sub

    Private Sub OnIKeyDown(sender As Object, e As KeyEventArgs)
        If e.Modifiers = ModifierKeys.Control Then
            PrintPlayerInfo()
        End If
    End Sub

    Private Async Sub PrintPlayerInfo()
        Dim player = My.Player.Status
        If player Is Nothing Then
            Console.WriteLine("Player not found")
            Return
        End If

        Dim hp = player.Value(EBGUAttrFloat.Hp)
        Dim hpMax = player.Value(EBGUAttrFloat.HpMax)
        Dim mp = player.Value(EBGUAttrFloat.Mp)
        Dim mpMax = player.Value(EBGUAttrFloat.MpMax)
        Dim sta = player.Value(EBGUAttrFloat.Stamina)
        Dim staMax = player.Value(EBGUAttrFloat.StaminaMax)

        Await MsgBoxAsync($"玩家状态：
生命值 {hp}/{hpMax}
法力值 {mp}/{mpMax}
耐力 {sta}/{staMax}")
    End Sub
End Class
