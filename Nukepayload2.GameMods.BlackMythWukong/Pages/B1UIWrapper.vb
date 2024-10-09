Imports b1
Imports B1UI
Imports B1UI.GSUI
Imports GSE.GSUI
Imports System.Reflection

Public MustInherit Class B1UIWrapper(Of TPage As GSUIPage)

    Protected MustOverride ReadOnly Property PageId As EnPageID

    Protected ReadOnly Property Page As TPage
        Get
            Dim worldContext = My.Player.Pawn

            Dim value = TryCast(GSUI.UIMgr.FindUIPage(worldContext, PageId), TPage)
            If value Is Nothing Then
                If Not AutoShowPage Then Return value
                Show()
                value = TryCast(GSUI.UIMgr.FindUIPage(worldContext, PageId), TPage)
            End If
            Return value
        End Get
    End Property

    Protected Overridable ReadOnly Property AutoShowPage As Boolean
        Get
            Return True
        End Get
    End Property

    Public Sub Show()
        BGUFunctionLibraryManaged.BGUSwitchPage(ActiveWorld, CType(PageId, EUIPageID))
    End Sub

    Protected Function TryGetField(Of TResult As Class)(fieldName As String) As TResult
        Dim instance = Page
        If instance Is Nothing Then Return Nothing
        Dim fldShrineMenuHelper = GetType(TPage).GetField(fieldName, BindingFlags.NonPublic Or BindingFlags.Instance)
        If fldShrineMenuHelper Is Nothing Then Return Nothing
        Return TryCast(fldShrineMenuHelper.GetValue(instance), TResult)
    End Function

    Protected Function TryGetFieldValue(Of TResult As Structure)(fieldName As String) As TResult?
        Dim instance = Page
        If instance Is Nothing Then Return Nothing
        Dim fldShrineMenuHelper = GetType(TPage).GetField(fieldName, BindingFlags.NonPublic Or BindingFlags.Instance)
        If fldShrineMenuHelper Is Nothing Then Return Nothing
        Dim result = fldShrineMenuHelper.GetValue(instance)
        If TypeOf result Is TResult Then
            Return DirectCast(result, TResult)
        End If
        Return Nothing
    End Function

End Class
