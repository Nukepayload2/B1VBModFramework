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
    Private _miniGmComm As UBorder

    Public Sub Show()
        _miniGm.Show()
        SubmitButton = _miniGm.BtnRunCmd
        CloseButton = _miniGm.CloseButton
        Dim submitTextBlock = TryCast(SubmitButton.GetContent, UTextBlock)
        submitTextBlock?.SetText("确定".AsFText)
        _miniGm.GMCmd.SetHintText("请在此输入文本".AsFText)
        Dim page = _miniGm.Page
        Dim children = page.GetChildViewListInfo
        Dim tabIndex = 0
        For Each childPage In children
            Select Case childPage?.GetType
                Case GetType(B1UI.GSUI.VIMiniGMPanel)
                    DirectCast(childPage, B1UI.GSUI.VIMiniGMPanel).ShowOut()
                Case GetType(B1UI.GSUI.VIMiniGMTab)
                    DirectCast(childPage, B1UI.GSUI.VIMiniGMTab).SetVisable(False)
                Case GetType(B1UI.GSUI.VIMiniGMQuickPanel)
                    DirectCast(childPage, B1UI.GSUI.VIMiniGMQuickPanel).ShowOut()
            End Select
        Next

        _miniGm.GMCmd.OnTextCommitted.Clear()
        _miniGm.GMCmd.OnTextCommitted.Bind(AddressOf OnEnterPressedInTextBox)
        Dim centerCon = TryCast(_miniGm.CenterCon, UCanvasPanel)
        _miniGmComm = Nothing
        If centerCon IsNot Nothing Then
            Dim stkContent = CreateVerticalStackPanel(centerCon)
            stkContent.AddChildToVerticalBox(CreateTitleTextBlock(centerCon))
            stkContent.AddChildToVerticalBox(CreateDescriptionTextBlock(centerCon))
            centerCon.AddChildToCanvas(stkContent).SetPosition(New FVector2D(12, 12))
            Dim comm = VisualTreeHelper.GetDescendants(centerCon).OfType(Of UBorder).FirstOrDefault
            If comm?.GetName = "Comm" Then
                _miniGmComm = comm
                comm.SetVisibility(ESlateVisibility.Collapsed)
            End If
        End If

        Dim tblLog = _miniGm.LogScrollBox.GetAllChildren().OfType(Of UTextBlock).FirstOrDefault
        tblLog?.SetText("提示：按回车或者点击确定按钮提交输入的文本。按右上角的 × 按钮取消。".AsFText)
    End Sub

    Private Function CreateVerticalStackPanel(panel As UPanelWidget) As UVerticalBox
        Return UObject.NewObject(Of UVerticalBox)(panel)
    End Function

    Private Function CreateTitleTextBlock(panel As UPanelWidget) As UTextBlock
        Dim tbl = UObject.NewObject(Of UTextBlock)(panel)
        tbl.SetText(If(Title, String.Empty).AsFText)
        Dim fntTitle = tbl.Font
        fntTitle.Size *= 3
        tbl.SetFont(fntTitle)
        Return tbl
    End Function

    Private Function CreateDescriptionTextBlock(panel As UPanelWidget) As UTextBlock
        Dim tbl = UObject.NewObject(Of UTextBlock)(panel)
        tbl.SetText(If(Description, String.Empty).AsFText)
        Dim fntTitle = tbl.Font
        fntTitle.Size *= 2
        tbl.SetFont(fntTitle)
        ' 打开自动换行会导致每个词都换行。设置 HorizontalAlignment=Fill 又没反应。
        ' tbl.SetAutoWrapText(True)
        Return tbl
    End Function

    Private Sub OnEnterPressedInTextBox(Text As FText, CommitMethod As ETextCommit)
        If CommitMethod <> ETextCommit.OnEnter Then Return
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
        _miniGmComm?.SetVisibility(ESlateVisibility.Visible)
        _miniGm.Close()
        DialogResultSource.SetResult(True)
    End Sub

    Private Sub CloseButton_OnGSButtonClicked(GSID As Integer) Handles CloseButton.OnGSButtonClicked
        _miniGmComm?.SetVisibility(ESlateVisibility.Visible)
        DialogResultSource.SetResult(False)
    End Sub
End Class
