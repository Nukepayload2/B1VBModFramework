Imports UnrealEngine.InputCore

Class MenuAndMessageBoxTests
    Inherits ModComponentBase

    Private ReadOnly BtnMsgBoxConfirmStyle As New MenuItem("BtnMsgBoxConfirmStyle") With {.Text = "普通确认", .Tooltip = "显示测试消息框，使用普通确认样式"}
    WithEvents BtnMsgBoxConfirmOkOnly As New MenuItem("BtnMsgBoxConfirmOkOnly") With {.Text = "仅确认", .Tooltip = "显示测试消息框，使用普通确认样式，只显示确认按钮"}
    WithEvents BtnMsgBoxConfirmOkCancel As New MenuItem("BtnMsgBoxConfirmOkCancel") With {.Text = "确认和取消", .Tooltip = "显示测试消息框，使用普通确认样式，显示确认和取消按钮"}

    Private ReadOnly BtnMsgBoxConfirmThreeStyle As New MenuItem("BtnMsgBoxConfirmThreeStyle") With {.Text = "3键确认", .Tooltip = "显示测试消息框，按返回键可以直接关闭"}
    WithEvents BtnMsgBoxConfirmThreeOkOnly As New MenuItem("BtnMsgBoxConfirmThreeOkOnly") With {.Text = "仅确认", .Tooltip = "显示测试消息框，按返回键可以直接关闭，只显示确认按钮"}
    WithEvents BtnMsgBoxConfirmThreeOkCancel As New MenuItem("BtnMsgBoxConfirmThreeOkCancel") With {.Text = "确认和取消", .Tooltip = "显示测试消息框，按返回键可以直接关闭，显示确认和取消按钮"}

    Private ReadOnly BtnMsgBoxGmConfirmStyle As New MenuItem("BtnMsgBoxGmConfirmStyle") With {.Text = "Gm确认", .Tooltip = "显示测试消息框，使用GM样式"}
    WithEvents BtnMsgBoxGmConfirmOkOnly As New MenuItem("BtnMsgBoxGmConfirmOkOnly") With {.Text = "Gm仅确认", .Tooltip = "显示测试消息框，使用GM样式，只显示确认按钮"}
    WithEvents BtnMsgBoxGmConfirmOkCancel As New MenuItem("BtnMsgBoxGmConfirmOkCancel") With {.Text = "Gm确认和取消", .Tooltip = "显示测试消息框，使用GM样式，显示确认和取消按钮"}

    Private ReadOnly BtnMsgBoxGm820ConfirmStyle As New MenuItem("BtnMsgBoxGm820ConfirmStyle") With {.Text = "Gm820确认", .Tooltip = "显示测试消息框，使用GM820样式"}
    WithEvents BtnMsgBoxGm820ConfirmOkOnly As New MenuItem("BtnMsgBoxGm820ConfirmOkOnly") With {.Text = "Gm820仅确认", .Tooltip = "显示测试消息框，使用GM820样式，只显示确认按钮"}
    WithEvents BtnMsgBoxGm820ConfirmOkCancel As New MenuItem("BtnMsgBoxGm820ConfirmOkCancel") With {.Text = "Gm820确认和取消", .Tooltip = "显示测试消息框，使用GM820样式，显示确认和取消按钮"}

    Private ReadOnly BtnMsgBoxShaderCompileConfirmStyle As New MenuItem("BtnMsgBoxShaderCompileConfirmStyle") With {.Text = "着色器编译确认", .Tooltip = "显示测试消息框，带有帮助按钮"}
    WithEvents BtnMsgBoxShaderCompileConfirmOkOnly As New MenuItem("BtnMsgBoxShaderCompileConfirmOkOnly") With {.Text = "仅确认", .Tooltip = "显示测试消息框，带有帮助按钮，只显示确认按钮"}
    WithEvents BtnMsgBoxShaderCompileConfirmOkCancel As New MenuItem("BtnMsgBoxShaderCompileConfirmOkCancel") With {.Text = "确认和取消", .Tooltip = "显示测试消息框，带有帮助按钮，显示确认和取消按钮"}

    WithEvents BtnMsgBoxCommErrTipsStyle As New MenuItem("BtnMsgBoxCommErrTipsStyle") With {.Text = "通信错误提示", .Tooltip = "显示测试消息框，表示通信错误"}

    Private ReadOnly BtnTips As New MenuItem("BtnTips") With {.Text = "通知消息", .Tooltip = "显示通知消息"}
    WithEvents BtnTipWarning As New MenuItem("BtnTipWarning") With {.Text = "通知消息：警告", .Tooltip = "显示通知消息，使用警告样式"}
    WithEvents BtnTipInformation As New MenuItem("BtnTipInformation") With {.Text = "通知消息：信息", .Tooltip = "显示通知消息，使用信息样式"}
    WithEvents BtnTipReset As New MenuItem("BtnTipReset") With {.Text = "通知消息：重置", .Tooltip = "显示通知消息，使用重置样式"}

    WithEvents MKey As InputManager.KeyOrButton

    Private Sub MenuAndMessageBoxTests_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddMenuItems()
        MKey = My.Computer.Keyboard.Keys(EKeys.M)
        Console.WriteLine("Press Ctrl+M to show menu test.")
    End Sub

    Private Sub AddMenuItems()
        BtnMsgBoxConfirmStyle.Items.AddRange({BtnMsgBoxConfirmOkOnly, BtnMsgBoxConfirmOkCancel})
        BtnMsgBoxConfirmThreeStyle.Items.AddRange({BtnMsgBoxConfirmThreeOkOnly, BtnMsgBoxConfirmThreeOkCancel})
        BtnMsgBoxGmConfirmStyle.Items.AddRange({BtnMsgBoxGmConfirmOkOnly, BtnMsgBoxGmConfirmOkCancel})
        BtnMsgBoxGm820ConfirmStyle.Items.AddRange({BtnMsgBoxGm820ConfirmOkOnly, BtnMsgBoxGm820ConfirmOkCancel})
        BtnMsgBoxShaderCompileConfirmStyle.Items.AddRange({BtnMsgBoxShaderCompileConfirmOkOnly, BtnMsgBoxShaderCompileConfirmOkCancel})
        BtnTips.Items.AddRange({BtnTipWarning, BtnTipInformation, BtnTipReset})

        With My.Window.ContextMenu
            .Title = "消息框测试"
            .Subtitle = "弹出各种各样的消息框来进行测试"

            With .Items
                .Add(BtnMsgBoxConfirmStyle)
                .Add(BtnMsgBoxConfirmThreeStyle)
                .Add(BtnMsgBoxGmConfirmStyle)
                .Add(BtnMsgBoxGm820ConfirmStyle)
                .Add(BtnMsgBoxShaderCompileConfirmStyle)
                .Add(BtnMsgBoxCommErrTipsStyle)
                .Add(BtnTips)
            End With
        End With
    End Sub

    Private Async Sub BtnMsgBoxCommErrTipsStyle_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxCommErrTipsStyle.Click
        Dim result = Await MsgBoxAsync("测试通信错误的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.CommErrTips})
        Console.WriteLine($"通信错误提示的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxConfirmOkCancel_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxConfirmOkCancel.Click
        Dim result = Await MsgBoxAsync("测试普通确认取消的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.Confirm, .HasCancelButton = True})
        Console.WriteLine($"普通确认取消的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxConfirmOkOnly_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxConfirmOkOnly.Click
        Dim result = Await MsgBoxAsync("测试普通确认的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.Confirm})
        Console.WriteLine($"普通确认的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxConfirmThreeOkCancel_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxConfirmThreeOkCancel.Click
        Dim result = Await MsgBoxAsync("测试3键确认取消的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.ConfirmThree, .HasCancelButton = True})
        Console.WriteLine($"3键确认取消的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxConfirmThreeOkOnly_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxConfirmThreeOkOnly.Click
        Dim result = Await MsgBoxAsync("测试3键确认的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.ConfirmThree})
        Console.WriteLine($"3键确认的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxGm820ConfirmOkCancel_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxGm820ConfirmOkCancel.Click
        Dim result = Await MsgBoxAsync("测试Gm820确认取消的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.Gm820Confirm, .HasCancelButton = True})
        Console.WriteLine($"Gm820确认取消的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxGm820ConfirmOkOnly_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxGm820ConfirmOkOnly.Click
        Dim result = Await MsgBoxAsync("测试Gm820确认的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.Gm820Confirm})
        Console.WriteLine($"Gm820确认的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxGmConfirmOkCancel_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxGmConfirmOkCancel.Click
        Dim result = Await MsgBoxAsync("测试Gm确认取消的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.GmConfirm, .HasCancelButton = True})
        Console.WriteLine($"Gm确认取消的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxGmConfirmOkOnly_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxGmConfirmOkOnly.Click
        Dim result = Await MsgBoxAsync("测试Gm确认的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.GmConfirm})
        Console.WriteLine($"Gm确认的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxShaderCompileConfirmOkCancel_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxShaderCompileConfirmOkCancel.Click
        Dim result = Await MsgBoxAsync("测试着色器编译确认取消的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.ShaderCompileConfirm, .HasCancelButton = True})
        Console.WriteLine($"着色器编译确认取消的返回值是 {result}")
    End Sub

    Private Async Sub BtnMsgBoxShaderCompileConfirmOkOnly_Click(sender As Object, e As EventArgs) Handles BtnMsgBoxShaderCompileConfirmOkOnly.Click
        Dim result = Await MsgBoxAsync("测试着色器编译确认的文本", New B1MsgBoxButtons With {.Style = B1MsgBoxStyle.ShaderCompileConfirm})
        Console.WriteLine($"着色器编译确认的返回值是 {result}")
    End Sub

    Private Sub BtnTipWarning_Click(sender As Object, e As EventArgs) Handles BtnTipWarning.Click
        ShowTip("测试警告文本")
    End Sub

    Private Sub BtnTipInformation_Click(sender As Object, e As EventArgs) Handles BtnTipInformation.Click
        ShowTip("测试消息文本", B1MsgBoxIcons.Information)
    End Sub

    Private Sub BtnTipReset_Click(sender As Object, e As EventArgs) Handles BtnTipReset.Click
        ShowTip("测试重置文本", B1MsgBoxIcons.Reset)
    End Sub

    Private Sub MKey_KeyDown(sender As Object, e As KeyEventArgs) Handles MKey.KeyDown
        If e.Modifiers = ModifierKeys.Control Then
            My.Window.ContextMenu.Show()
        End If
    End Sub
End Class
