Imports BasicModTest
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class ModFeaturesTest
    <TestMethod>
    Async Function TestAskAI() As Task

        Dim userMessage = $"生命值 {100}/{500}
法力值 {34}/{612}
耐力 {300}/{300}"

        Dim aiResponse = Await AskAIAsync("qwen2:latest", "http://localhost:11434/api/chat",
                         "你是游戏助手，你负责总结玩家状态值的情况。状态值的格式为：名称 当前数值/最大值。你需要对每个状态值做出简短的总结。",
                         userMessage)
        Assert.IsNotNull(aiResponse)
    End Function

    <TestMethod>
    Async Function TestAskAIOld() As Task

        Dim userMessage = $"生命值 {123}/{500}
法力值 {34}/{612}
耐力 {300}/{300}"

        Dim aiResponse = Await LegacyAskAIAsync("qwen2:latest", "http://localhost:11434/api/chat",
                         "你是游戏助手，你负责总结玩家状态值的情况。状态值的格式为：名称 当前数值/最大值。你需要对每个状态值做出简短的总结。",
                         userMessage)
        Assert.IsNotNull(aiResponse)
    End Function
End Class

