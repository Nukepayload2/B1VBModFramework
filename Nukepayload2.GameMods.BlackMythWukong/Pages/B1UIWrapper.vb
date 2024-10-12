Imports b1
Imports B1UI
Imports B1UI.GSUI
Imports GSE.GSUI
Imports System.Reflection
Imports UnrealEngine.Runtime
Imports UnrealEngine.UMG

Public MustInherit Class B1UIWrapper(Of TPage As GSUIPage)

    Protected MustOverride ReadOnly Property PageId As EnPageID

    Public ReadOnly Property Page As TPage
        Get
            Dim worldContext = My.Player.Pawn
            Return TryCast(GSUI.UIMgr.FindUIPage(worldContext, PageId), TPage)
        End Get
    End Property

    Public Sub Show()
        Dim worldContext = My.Player.Pawn
        Dim value = TryCast(GSUI.UIMgr.FindUIPage(worldContext, PageId), TPage)
        If value Is Nothing Then
            BGUFunctionLibraryManaged.BGUSwitchPage(ActiveWorld, CType(PageId, EUIPageID))
        End If
    End Sub

    Public Sub Close()
        Dim worldContext = My.Player.Pawn
        Dim value = TryCast(GSUI.UIMgr.FindUIPage(worldContext, PageId), TPage)
        If value IsNot Nothing Then
            GenAGPage.FadeOutPage(PageId, "OnClickCloseBtn")
        End If
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

    Public ReadOnly Property OwnerPage As GSUIPage
        Get
            Return Page?.GetOwnerPage
        End Get
    End Property

    Public ReadOnly Property RootUserWidget As UUserWidget
        Get
            Return Page?.GetRootUserWidget
        End Get
    End Property

    Public Function FindControl(Of T As UWidget)(name As String) As T
        Return FindControl(Of T)(RootUserWidget, name)
    End Function

    Public Function FindControl(Of T As UWidget)(lookIn As UUserWidget, name As String) As T
        name.RequireNotNull(NameOf(name))
        Return TryCast(UGSE_UMGFuncLib.GetWidgetFromName(lookIn, New FName(name)), T)
    End Function

End Class
