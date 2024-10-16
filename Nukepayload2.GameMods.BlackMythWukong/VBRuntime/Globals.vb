Imports UnrealEngine.Engine
Imports UnrealEngine.Runtime

' VB6 的时候有个传统，类库嵌入应用程序时，应当能以最方便的方式访问全局对象
Public Module Globals

    ''' <summary>
    ''' 获取当前世界
    ''' </summary>
    Public ReadOnly Property ActiveWorld As UWorld
        Get
            Dim uobjectRef As UObjectRef = GCHelper.FindRef(FGlobals.GWorld)
            Return TryCast(uobjectRef?.Managed, UWorld)
        End Get
    End Property

End Module
