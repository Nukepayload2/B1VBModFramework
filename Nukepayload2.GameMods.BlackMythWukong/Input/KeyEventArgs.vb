Imports b1
Imports UnrealEngine.Engine
Imports UnrealEngine.InputCore

Public Class KeyEventArgs
    Inherits EventArgs

    Public Sub New(key As EKeys, modifiers As ModifierKeys, world As UWorld, player As BGP_PlayerControllerB1)
        Me.Key = key
        Me.Modifiers = modifiers
        Me.World = world
        Me.Player = player
    End Sub

    Public ReadOnly Property Key As EKeys
    Public ReadOnly Property Modifiers As ModifierKeys
    Public ReadOnly Property World As UWorld
    Public ReadOnly Property Player As BGP_PlayerControllerB1
End Class
