Imports System.Runtime.CompilerServices
Imports UnrealEngine.Runtime

Public Module B1Conversion

    <Extension>
    Public Function AsFText(value As String) As FText
        If value Is Nothing Then Return Nothing
        Return FText.FromString(value)
    End Function

    <Extension>
    Public Sub DefaultsTo(Of T As Class)(ByRef value As T, fallback As T)
        value = If(value, fallback)
    End Sub
End Module
