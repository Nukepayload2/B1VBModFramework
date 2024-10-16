Imports b1.BGW
Imports UnrealEngine.Engine
Imports UnrealEngine.Runtime

Namespace MyService
    Public Class WorldProxy

        Public ReadOnly Property Instance As UWorld
            Get
                Dim uobjectRef As UObjectRef = GCHelper.FindRef(FGlobals.GWorld)
                Return TryCast(uobjectRef?.Managed, UWorld)
            End Get
        End Property

        Public Function LoadAsset(Of T As UObject)(asset As String) As T
            Return BGW_PreloadAssetMgr.Get(Instance).TryGetCachedResourceObj(Of T)(
            asset, ELoadResourceType.SyncLoadAndCache, EAssetPriority.Default,
            Nothing, -1, -1)
        End Function

    End Class

End Namespace
