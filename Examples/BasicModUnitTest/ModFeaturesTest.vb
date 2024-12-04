Imports System.Text
Imports BasicModTest
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Nukepayload2.GameMods.BlackMythWukong.Logging

<TestClass>
Public Class ModFeaturesTest
    <TestMethod>
    Async Function TestAskAI() As Task

        Dim userMessage = $"生命值 {100}/{500}
法力值 {34}/{612}
耐力 {300}/{300}"

        Dim aiResponse = Await AskLocalAIAsync("qwen2:latest", "http://localhost:11434/api/chat",
                         "你是游戏助手，你负责总结玩家状态值的情况。状态值的格式为：名称 当前数值/最大值。你需要对每个状态值做出简短的总结。",
                         userMessage)
        Assert.IsNotNull(aiResponse)
    End Function

    <TestMethod>
    Async Function TestZhipuAskAI() As Task
        Dim userMessage = $"生命值 {100}/{500}
法力值 {34}/{612}
耐力 {300}/{300}"

        Dim aiResponse = Await AskZhipuAIAsync(
                         "你是游戏助手，你负责总结玩家状态值的情况。状态值的格式为：名称 当前数值/最大值。你需要对每个状态值做出简短的总结。",
                         userMessage)
        Console.WriteLine(aiResponse)
        Assert.IsNotNull(aiResponse)
    End Function

    <TestMethod>
    Public Sub LogTest()
        Dim logger As New Log
        Dim stub As New TraceListenerStub
        With logger.TraceSource.Listeners
            .Clear()
            .Add(stub)
        End With
        logger.WriteEntry("This is a test 1234567890")
        Dim logContent = stub.Content.ToString
        Assert.IsTrue(logContent.Contains("This is a test 1234567890"))
    End Sub

    Private Class TraceListenerStub
        Inherits TraceListener

        Public ReadOnly Property Content As New StringBuilder

        Public Overrides Sub Write(message As String)
            Content.Append(message)
        End Sub

        Public Overrides Sub WriteLine(message As String)
            Content.AppendLine(message)
        End Sub
    End Class
End Class

