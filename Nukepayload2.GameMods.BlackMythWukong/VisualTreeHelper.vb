Imports UnrealEngine.UMG

Public Class VisualTreeHelper
    Public Shared Iterator Function GetParents(widget As UWidget) As IEnumerable(Of UPanelWidget)
        Dim current = widget.GetParent
        Do Until current Is Nothing
            Yield current
            current = current.GetParent
        Loop
    End Function

    Public Shared Sub PrintVisualTree(contentRoot As UUserWidget, partName As String)
        Console.WriteLine($"{partName} is {contentRoot.GetName} As {contentRoot.GetType.FullName}")
        Dim index = 1
        For Each curCtl In GetParents(contentRoot)
            Console.WriteLine($"  Ancestor level {index} is {curCtl.GetName} As {curCtl.GetType.FullName}")
            index += 1
        Next
    End Sub

End Class
