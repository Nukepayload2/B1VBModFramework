Imports System.IO
Imports b1
Imports UnrealEngine.UMG

Public Class VisualTreeHelper
    Public Shared Iterator Function GetParents(widget As UWidget) As IEnumerable(Of UPanelWidget)
        Dim current = widget.GetParent
        Do Until current Is Nothing
            Yield current
            current = current.GetParent
        Loop
    End Function

    Public Shared Function GetChildren(widget As UWidget) As IEnumerable(Of UWidget)
        If TypeOf widget Is UUserWidget Then
            Return UGSE_UMGFuncLib.GetAllChildWidget(widget)
        ElseIf TypeOf widget Is UPanelWidget Then
            Return DirectCast(widget, UPanelWidget).GetAllChildren
        Else
            Return Enumerable.Empty(Of UWidget)
        End If
    End Function

    Public Shared Function GetDescendants(widget As UWidget) As IEnumerable(Of UWidget)
        Return GetDescendantsAndSelf(widget).Skip(1)
    End Function

    Public Shared Iterator Function GetDescendantsAndSelf(widget As UWidget) As IEnumerable(Of UWidget)
        Dim ctls As New Stack(Of UWidget)
        ctls.Push(widget)
        Do While ctls.Count > 0
            Dim stackFrame = ctls.Pop
            Yield stackFrame
            For Each child In GetChildren(stackFrame)
                ctls.Push(child)
            Next
        Loop
    End Function

    Public Shared Iterator Function GetDescendantsAndSelfWithDepth(widget As UWidget) As IEnumerable(Of (Control As UWidget, Depth As Integer))
        Dim ctls As New Stack(Of (ctl As UWidget, depth As Integer))
        ctls.Push((widget, 0))
        Do While ctls.Count > 0
            Dim stackFrame = ctls.Pop
            Yield stackFrame
            Dim curDepth = stackFrame.depth
            For Each child In GetChildren(stackFrame.ctl)
                ctls.Push((child, curDepth + 1))
            Next
        Loop
    End Function

    Public Shared Sub PrintDescendantsAsUnsafeXml(root As UWidget, target As TextWriter)
        Dim stack As New Stack(Of (node As UWidget, indentLevel As Integer, isFirst As Boolean))
        stack.Push((root, 0, True))

        While stack.Count > 0
            With stack.Pop()
                Dim children = GetChildren(.node).ToArray
                Dim indent = New String(" "c, .indentLevel * 2)
                If .isFirst Then
                    Dim quickClose = If(children.Length = 0, "/", String.Empty)
                    target.WriteLine($"{indent}<{ .node.GetType.FullName} Name=""{ .node.GetName}""{quickClose}>")
                    If children.Length > 0 Then
                        stack.Push((.node, .indentLevel, False))
                        For Each child In children
                            stack.Push((child, .indentLevel + 1, True))
                        Next
                    End If
                Else
                    target.WriteLine($"{indent}</{ .node.GetType.FullName}>")
                End If
            End With
        End While
    End Sub

    Public Shared Sub PrintParents(contentRoot As UWidget, partName As String, target As TextWriter)
        target.WriteLine($"{partName} is {contentRoot.GetName} As {contentRoot.GetType.FullName}")
        Dim index = 1
        For Each curCtl In GetParents(contentRoot)
            target.WriteLine($"  Ancestor level {index} is {curCtl.GetName} As {curCtl.GetType.FullName}")
            index += 1
        Next
    End Sub

End Class
