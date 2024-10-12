Imports B1UI.GSUI
Imports ResB1

Public Class MenuFlyout

    Private ReadOnly _shrineMain As New B1ShrineMain

    Public Sub Show()
        _shrineMain.Show()

        If Title IsNot Nothing Then
            _shrineMain.TxtMainName?.SetText(Title.AsFText)
        End If
        If Subtitle IsNot Nothing Then
            _shrineMain.TxtSubName?.SetText(Subtitle.AsFText)
        End If

        Dim fMenuHelper = _shrineMain.ShrineMenuHelper
        If fMenuHelper Is Nothing Then
            Return
        End If

        fMenuHelper.ClearLayout()
        Arrange(fMenuHelper)
        fMenuHelper.UpdateLayout()
    End Sub

    Protected Sub Arrange(helper As FMenuHelper(Of EShrineMenuTag))
        Dim tabIndex = 0
        For Each item In Items
            item.Accept(Nothing, tabIndex, AddressOf helper.Register)
        Next
    End Sub

    Public ReadOnly Property Items As New List(Of MenuItem)
    Public Property Title As String
    Public Property Subtitle As String
End Class

Public Class MenuItem
    Public Sub New(name As String)
        Me.Name = name
    End Sub

    Public ReadOnly Property Name As String

    Public Property Text As String
    Public Property Tooltip As String

    Public Event Click As EventHandler

    Public ReadOnly Property Items As New List(Of MenuItem)

    Public Sub Accept(parent As MenuItem, ByRef tabIndex As Integer, register As Action(Of FBtnRegisterInfo(Of EShrineMenuTag)))
        Dim regInfo As New FBtnRegisterInfo(Of EShrineMenuTag) With {
            .MenuBtnType = EMenuBtnType.Func,
            .BtnActionType = EMenuBtnActionType.Teleport,
            .Name = Text.AsFText,
            .Tips = Tooltip.AsFText,
            .BtnAction = If(ClickEvent Is Nothing, Nothing, Sub(it As DSItemEntry) RaiseEvent Click(Me, EventArgs.Empty)),
            .BtnHashCode = Name,
            .SortId = tabIndex,
            .ParentBtnHash = parent?.Name
        }
        tabIndex += 1
        register(regInfo)
        For Each item In Items
            item.Accept(Me, tabIndex, register)
        Next
    End Sub
End Class
