Imports B1UI.GSUI

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
