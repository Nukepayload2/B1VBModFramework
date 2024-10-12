Imports B1UI.GSUI

Public Module B1Interaction
    Async Function MsgBoxAsync(prompt As String, Optional buttons As B1MsgBoxButtons = Nothing, Optional title As String = Nothing) As Task(Of B1MsgBoxResult)
        buttons.DefaultsTo(B1MsgBoxButtons.Ok)
        Dim cts As New TaskCompletionSource(Of B1MsgBoxResult)
        Dim fTitle = title.AsFText
        Dim fPrompt = prompt.AsFText
        Dim fOk = buttons.OkButtonText.AsFText
        Dim fCancel = buttons.CancelButtonText.AsFText
        Dim fBack = buttons.BackButtonText.AsFText
        Dim onOk = Function()
                       cts.SetResult(B1MsgBoxResult.Ok)
                       Return True
                   End Function
        Dim onCancel = Function()
                           cts.SetResult(B1MsgBoxResult.Cancel)
                           Return True
                       End Function
        Dim onBack = Function()
                         cts.SetResult(B1MsgBoxResult.Back)
                         Return True
                     End Function
        Dim hasCancel = buttons.HasCancelButton
        Select Case buttons.Style
            Case B1MsgBoxStyle.Confirm
                GSB1UIUtil.ShowConfirm(
                    fTitle, fPrompt, fOk, fCancel, hasCancel, onOk, onCancel)
            Case B1MsgBoxStyle.ConfirmThree
                GSB1UIUtil.ShowConfirmThree(
                    fTitle, fPrompt, fOk, fCancel,
                    fBack, hasCancel, onOk, onCancel, onBack)
            Case B1MsgBoxStyle.GmConfirm
                GSB1UIUtil.ShowGMConfirm(
                    fTitle, fPrompt, fOk, fCancel, hasCancel, onOk, onCancel)
            Case B1MsgBoxStyle.Gm820Confirm
                GSB1UIUtil.Show820GMConfirm(
                    fTitle, fPrompt, fOk, fCancel, hasCancel, onOk, onCancel)
            Case B1MsgBoxStyle.ShaderCompileConfirm
                GSB1UIUtil.ShowShaderCompileConfirm(
                    fTitle, fPrompt, fOk, fCancel,
                    fBack, hasCancel, onOk, onCancel, onBack)
            Case B1MsgBoxStyle.CommErrTips
                GSB1UIUtil.ShowCommErrTips(prompt)
                cts.SetResult(B1MsgBoxResult.Ok)
            Case Else
                Return B1MsgBoxResult.Cancel
        End Select
        Return Await cts.Task
    End Function

    Sub ShowTip(prompt As String, Optional icon As B1MsgBoxIcons = B1MsgBoxIcons.Warning)
        GSB1UIUtil.ShowGMCommTips(prompt.AsFText, icon)
    End Sub

    Async Function InputBoxAsync(prompt As String, title As String) As Task(Of String)
        Dim page As New InputBoxPage With {
            .Description = prompt, .Title = title
        }
        Dim dlgResult = Await page.ShowDialogAsync
        If dlgResult Then
            Return page.Text
        End If
        Return Nothing
    End Function
End Module

Public Enum B1MsgBoxIcons
    Warning
    Information
    Reset
End Enum

Public Class B1MsgBoxButtons
    Public Shared ReadOnly Property Ok As New B1MsgBoxButtons With {
        .HasCancelButton = False
    }
    Public Shared ReadOnly Property OkCancel As New B1MsgBoxButtons With {
        .HasCancelButton = True
    }

    Public Property HasCancelButton As Boolean
    Public Property OkButtonText As String = "确定"
    Public Property CancelButtonText As String = "取消"
    Public Property BackButtonText As String = "返回"
    Public Property Style As B1MsgBoxStyle
End Class

Public Enum B1MsgBoxStyle
    Confirm
    ConfirmThree
    GmConfirm
    Gm820Confirm
    ShaderCompileConfirm
    CommErrTips
End Enum

Public Enum B1MsgBoxResult
    Ok
    Cancel
    Back
End Enum
