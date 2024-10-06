Imports BasicModTest
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class ModFeaturesTest
    <TestMethod>
    Async Function TestAskAI() As Task

        Dim userMessage = $"����ֵ {100}/{500}
����ֵ {34}/{612}
���� {300}/{300}"

        Dim aiResponse = Await AskAIAsync("qwen2:latest", "http://localhost:11434/api/chat",
                         "������Ϸ���֣��㸺���ܽ����״ֵ̬�������״ֵ̬�ĸ�ʽΪ������ ��ǰ��ֵ/���ֵ������Ҫ��ÿ��״ֵ̬������̵��ܽᡣ",
                         userMessage)
        Assert.IsNotNull(aiResponse)
    End Function

    <TestMethod>
    Async Function TestAskAIOld() As Task

        Dim userMessage = $"����ֵ {123}/{500}
����ֵ {34}/{612}
���� {300}/{300}"

        Dim aiResponse = Await LegacyAskAIAsync("qwen2:latest", "http://localhost:11434/api/chat",
                         "������Ϸ���֣��㸺���ܽ����״ֵ̬�������״ֵ̬�ĸ�ʽΪ������ ��ǰ��ֵ/���ֵ������Ҫ��ÿ��״ֵ̬������̵��ܽᡣ",
                         userMessage)
        Assert.IsNotNull(aiResponse)
    End Function
End Class

