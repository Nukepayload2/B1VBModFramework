Imports System.Reflection
Imports CSharpModBase

Partial Class BasicMod1
    Implements ICSharpMod

    ' 这俩属性是 MSBuild 指定的而不是写代码里面的，不然 CI 构建麻烦。
    Public ReadOnly Property Name As String Implements ICSharpMod.Name
        Get
            Return GetType(BasicMod1).Assembly.GetCustomAttribute(Of AssemblyTitleAttribute)?.Title
        End Get
    End Property

    Public ReadOnly Property Version As String Implements ICSharpMod.Version
        Get
            Return GetType(BasicMod1).Assembly.GetCustomAttribute(Of AssemblyInformationalVersionAttribute)?.InformationalVersion
        End Get
    End Property

    Event Load As EventHandler
    Event Unload As EventHandler

    Public ReadOnly Property Components As New List(Of ModComponentBase)

    Sub New()
        My.Mod = Me
        InitializeComponents()
    End Sub

    Partial Private Sub InitializeComponents()
        ' 在另一个文件里面，用于初始化组件。这个过程可以是源生成器写的，也可以是人写的。
    End Sub

    Public Sub Init() Implements ICSharpMod.Init
        GamePlayDispatcher.Register()
        B1SynchronizationContext.TryInitForGameThread()
        My.Computer.InputManager.StartListening()
        RaiseEvent Load(Me, EventArgs.Empty)
    End Sub

    Public Sub DeInit() Implements ICSharpMod.DeInit
        RaiseEvent Unload(Me, EventArgs.Empty)
        My.Computer.InputManager.StopListening()
    End Sub

End Class

MustInherit Class ModComponentBase

    Custom Event Load As EventHandler
        AddHandler(value As EventHandler)
            AddHandler My.Mod.Load, value
        End AddHandler
        RemoveHandler(value As EventHandler)
            RemoveHandler My.Mod.Load, value
        End RemoveHandler
        RaiseEvent(sender As Object, e As EventArgs)
            Throw New NotSupportedException
        End RaiseEvent
    End Event

    Custom Event Unload As EventHandler
        AddHandler(value As EventHandler)
            AddHandler My.Mod.Unload, value
        End AddHandler
        RemoveHandler(value As EventHandler)
            RemoveHandler My.Mod.Unload, value
        End RemoveHandler
        RaiseEvent(sender As Object, e As EventArgs)
            Throw New NotSupportedException
        End RaiseEvent
    End Event

End Class
