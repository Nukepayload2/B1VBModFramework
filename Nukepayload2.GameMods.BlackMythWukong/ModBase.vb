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

    Public Event Load As EventHandler
    Public Event Unload As EventHandler

    Protected Overridable Sub OnLoad() Implements ICSharpMod.Init
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
