Imports b1.GSMUI.GSWidget
Imports UnrealEngine.Runtime
Imports UnrealEngine.SlateCore
Imports UnrealEngine.UMG

Public Class InputBoxPage
    Private ReadOnly _miniGm As New B1MiniGM

    Public Property Title As String
    Public Property Description As String

    Public ReadOnly Property Text As String

    Private WithEvents SubmitButton As GSButton
    Private WithEvents CloseButton As GSButton

    Public Sub Show()
        _miniGm.Show()
        SubmitButton = _miniGm.BtnRunCmd
        CloseButton = _miniGm.CloseButton
        Dim submitTextBlock = TryCast(SubmitButton.GetContent, UTextBlock)
        submitTextBlock?.SetText("确定".AsFText)
        _miniGm.GMCmd.SetHintText("请在此输入文本".AsFText)
        Dim page = _miniGm.Page
        Dim children = page.GetChildViewListInfo
        For Each childPage In children
            Select Case childPage?.GetType
                Case GetType(B1UI.GSUI.VIMiniGMPanel)
                    DirectCast(childPage, B1UI.GSUI.VIMiniGMPanel).GSSetVisiable(False)
                Case GetType(B1UI.GSUI.VIMiniGMTab)
                    DirectCast(childPage, B1UI.GSUI.VIMiniGMTab).SetVisable(True)
                Case GetType(B1UI.GSUI.VIMiniGMQuickPanel)
                    DirectCast(childPage, B1UI.GSUI.VIMiniGMQuickPanel).ShowOut()
            End Select
        Next
        _miniGm.GMCmd.OnTextCommitted.Bind(AddressOf OnEnterPressedInTextBox)

        Dim contentRoot = _miniGm.RootUserWidget
        Console.WriteLine($"Root user widget is {contentRoot?.GetType.FullName}")
        Dim contentRootExact = TryCast(contentRoot, b1.UI.BUI_Widget)
    End Sub

    Private Sub OnEnterPressedInTextBox(Text As FText, CommitMethod As ETextCommit)
        _miniGm.GMCmd.OnTextCommitted.Unbind(AddressOf OnEnterPressedInTextBox)
        Dim txt = Text?.ToString
        GamePlayDispatcher.Instance.BeginInvoke(
        Sub()
            _Text = txt
            Commit()
        End Sub)
    End Sub

    Public Async Function ShowDialogAsync() As Task(Of Boolean?)
        Show()
        Return Await DialogResultSource.Task
    End Function

    Public ReadOnly Property DialogResultSource As New TaskCompletionSource(Of Boolean?)

    Private Sub SubmitButton_OnGSButtonClicked(GSID As Integer) Handles SubmitButton.OnGSButtonClicked
        _Text = _miniGm.GMCmd.GetText
        Commit()
    End Sub

    Private Sub Commit()
        _miniGm.Close()
        DialogResultSource.SetResult(True)
    End Sub

    Private Sub CloseButton_OnGSButtonClicked(GSID As Integer) Handles CloseButton.OnGSButtonClicked
        DialogResultSource.SetResult(False)
    End Sub
End Class
