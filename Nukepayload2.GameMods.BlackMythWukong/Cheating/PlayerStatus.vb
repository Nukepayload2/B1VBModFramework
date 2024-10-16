Imports b1
Imports BtlShare
Imports System.Runtime.CompilerServices
Imports UnrealEngine.Engine

Public Module PlayerStateExtensions
    <Extension>
    Function GetStatus(player As AActor) As PlayerStatus
        Return New PlayerStatus(player)
    End Function
End Module

Public Class PlayerStatus
    Private ReadOnly _player As AActor

    Public Sub New(player As AActor)
        _player = player
    End Sub

    Public Property Value(type As EBGUAttrFloat) As Single
        Get
            Return BGUFunctionLibraryCS.GetAttrValue(_player, type)
        End Get
        Set(value As Single)
            BGUFunctionLibraryCS.BGUSetAttrValue(_player, type, value)
        End Set
    End Property

    Public Sub AddBuff(buffId As BuffIds, duration As TimeSpan)
        BGUFunctionLibraryCS.BGUAddBuff(_player, _player, buffId, EBuffSourceType.GM, duration.TotalMilliseconds)
    End Sub

    Public Property IsSimpleStateEnabled(type As EBGUSimpleState) As Boolean
        Get
            Return BGUFunctionLibraryCS.BGUHasUnitSimpleState(_player, type)
        End Get
        Set(value As Boolean)
            BGUFunctionLibraryCS.BGUSetUnitSimpleState(_player, type, Not value)
        End Set
    End Property

End Class
