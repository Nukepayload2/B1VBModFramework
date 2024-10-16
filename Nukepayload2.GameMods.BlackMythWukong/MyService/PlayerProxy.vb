Imports b1

Namespace MyService
    Public Class PlayerProxy
        Public ReadOnly Property Controller As BGP_PlayerControllerB1
            Get
                Dim w = My.World.Instance
                If w Is Nothing Then Return Nothing
                Return TryCast(UGSE_EngineFuncLib.GetFirstLocalPlayerController(w), BGP_PlayerControllerB1)
            End Get
        End Property

        Public ReadOnly Property Pawn As BGUPlayerCharacterCS
            Get
                Dim c = Controller
                If c Is Nothing Then Return Nothing
                Return TryCast(c.GetControlledPawn, BGUPlayerCharacterCS)
            End Get
        End Property

        Public ReadOnly Property EventCollection As BUS_GSEventCollection
            Get
                Dim p = Pawn
                If p Is Nothing Then Return Nothing
                Return BUS_EventCollectionCS.Get(p)
            End Get
        End Property

        Public ReadOnly Property Status As PlayerStatus
            Get
                Dim p = Pawn
                If p Is Nothing Then Return Nothing
                Return New PlayerStatus(p)
            End Get
        End Property
    End Class

End Namespace
