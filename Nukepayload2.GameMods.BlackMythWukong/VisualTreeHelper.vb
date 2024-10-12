Imports UnrealEngine.UMG

Public Class VisualTreeHelper
    Public Shared Iterator Function GetParents(widget As UWidget) As IEnumerable(Of UPanelWidget)
        Dim current = widget.GetParent
        Do Until current Is Nothing
            Yield current
            current = current.GetParent
        Loop
    End Function
End Class
