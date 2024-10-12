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
        Dim tabIndex = 0
        For Each childPage In children
            Select Case childPage?.GetType
                Case GetType(B1UI.GSUI.VIMiniGMPanel)
                    DirectCast(childPage, B1UI.GSUI.VIMiniGMPanel).GSSetVisiable(False)
                Case GetType(B1UI.GSUI.VIMiniGMTab)
                    Dim tabControl = DirectCast(childPage, B1UI.GSUI.VIMiniGMTab)
                    tabControl.SetVisable(True)
                    Dim titleControl = tabControl.FindChildWidget("Title")
                    Dim utb = TryCast(titleControl, UTextBlock)
                    If utb IsNot Nothing Then
                        If tabIndex = 0 Then
                            If Title IsNot Nothing Then
                                utb.SetText(Title.AsFText)
                            End If
                        ElseIf tabIndex = 1 Then
                            tabControl.SetVisable(False)
                        End If

                        Console.WriteLine($"Title text is {utb.GetText}, tracing parents...")
                        Dim curCtl As UWidget = utb
                        Dim index = 0
                        Do
                            Console.WriteLine($"  Ancestor level {index } is {curCtl.GetName} As {curCtl.GetType.FullName}")
                            If TypeOf curCtl Is UOverlay Then
                                Dim overlay = DirectCast(curCtl, UOverlay)
                                Dim txtDesc2 = UObject.NewObject(Of UTextBlock)(overlay)
                                txtDesc2.SetText("试试动态添加一个控件？".AsFText)
                                overlay.AddChildToOverlay(txtDesc2)
                            End If
                            curCtl = curCtl.GetParent
                            index += 1
                        Loop Until curCtl Is Nothing
                    End If
                    tabIndex += 1
                Case GetType(B1UI.GSUI.VIMiniGMQuickPanel)
                    DirectCast(childPage, B1UI.GSUI.VIMiniGMQuickPanel).ShowOut()
            End Select
        Next
        _miniGm.GMCmd.OnTextCommitted.Clear()
        _miniGm.GMCmd.OnTextCommitted.Bind(AddressOf OnEnterPressedInTextBox)

        Dim desc = Description
        If desc IsNot Nothing Then
            Dim tblLog = _miniGm.LogScrollBox.GetAllChildren().OfType(Of UTextBlock).FirstOrDefault
            tblLog?.SetText(desc.AsFText)
        End If

        Dim contentRoot = _miniGm.RootUserWidget
        If contentRoot IsNot Nothing Then
            Console.WriteLine($"Root user widget is {contentRoot.GetName} As {contentRoot.GetType.FullName}")
            Dim index = 0
            For Each curCtl In VisualTreeHelper.GetParents(contentRoot)
                Console.WriteLine($"  Ancestor level {index } is {curCtl.GetName} As {curCtl.GetType.FullName}")
                index += 1
            Next
        End If
    End Sub

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
        _miniGm.Close()
        DialogResultSource.SetResult(True)
    End Sub

    Private Sub CloseButton_OnGSButtonClicked(GSID As Integer) Handles CloseButton.OnGSButtonClicked
        DialogResultSource.SetResult(False)
    End Sub
End Class
