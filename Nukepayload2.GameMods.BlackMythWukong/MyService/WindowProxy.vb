Imports B1UI.GSUI
Imports UnrealEngine.Engine
Imports UnrealEngine.Runtime

Namespace MyService

    Public Class WindowProxy
        ''' <summary>
        ''' 用于在游戏窗口中显示一个菜单
        ''' </summary>
        Public ReadOnly Property ContextMenu As New MenuFlyout

        Public ReadOnly Property CanEnterTakePhotoMode As Boolean
            Get
                Return GSB1UIUtil.GetIsCanEnterTakePhotoMode(My.World.Instance, True)
            End Get
        End Property

        Public Sub NavigateTo(pageName As String)
            pageName.RequireNotNull(NameOf(pageName))

            UGameplayStatics.OpenLevel(My.World.Instance, New FName(pageName))
        End Sub
    End Class

End Namespace
