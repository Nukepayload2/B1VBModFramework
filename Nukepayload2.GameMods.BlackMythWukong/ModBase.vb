Imports System.IO
Imports System.Reflection
Imports CSharpModBase

Public Class ModBase
    Implements ICSharpMod

    Public ReadOnly Property Name As String Implements ICSharpMod.Name
        Get
            Return GetType(ModBase).Assembly.GetCustomAttribute(Of AssemblyTitleAttribute)?.Title
        End Get
    End Property

    Public ReadOnly Property Components As New List(Of ModComponentBase)

    Public ReadOnly Property Version As String Implements ICSharpMod.Version
        Get
            Return GetType(ModBase).Assembly.GetCustomAttribute(Of AssemblyInformationalVersionAttribute)?.InformationalVersion
        End Get
    End Property

    Public ReadOnly Property BaseDirectory As String
        Get
            Return Path.Combine(My.Application.BaseDirectory, "CSharpLoader", "Mods", Name)
        End Get
    End Property

    ''' <summary>
    ''' 在模组启动阶段，初始化底层的服务。先于 <see cref="Load"/> 事件发生。
    ''' </summary>
    Public Event Initialized As EventHandler
    ''' <summary>
    ''' 在模组启动阶段，初始化每个部件中的游戏逻辑。在 <see cref="Initialized"/> 事件之后发生。
    ''' </summary>
    Public Event Load As EventHandler
    ''' <summary>
    ''' 在模组正常退出时发生此事件。不保证每次退出游戏都会执行它。
    ''' </summary>
    Public Event Unload As EventHandler

    Protected Sub OnInit() Implements ICSharpMod.Init
        OnInitialized()
        OnLoad()
    End Sub

    Protected Overridable Sub OnInitialized()
        RaiseEvent Initialized(Me, EventArgs.Empty)
    End Sub

    Protected Overridable Sub OnLoad()
        RaiseEvent Load(Me, EventArgs.Empty)
    End Sub

    Protected Overridable Sub OnUnload() Implements ICSharpMod.DeInit
        RaiseEvent Unload(Me, EventArgs.Empty)
    End Sub

End Class

Public MustInherit Class ModComponentBase

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
