Imports System.Net.Http
Imports System.Text
Imports b1
Imports BtlShare
Imports Newtonsoft.Json

Class AITest
    Inherits ModComponentBase

    Private WithEvents AKey As InputManager.KeyOrButton

    Private Async Sub PrintPlayerInfoAI()
        Dim player = My.Player.Pawn
        If player Is Nothing Then
            Console.WriteLine("Player not found")
            Return
        End If

        Dim hp = BGUFunctionLibraryCS.GetAttrValue(player, EBGUAttrFloat.Hp)
        Dim hpMax = BGUFunctionLibraryCS.GetAttrValue(player, EBGUAttrFloat.HpMax)
        Dim mp = BGUFunctionLibraryCS.GetAttrValue(player, EBGUAttrFloat.Mp)
        Dim mpMax = BGUFunctionLibraryCS.GetAttrValue(player, EBGUAttrFloat.MpMax)
        Dim sta = BGUFunctionLibraryCS.GetAttrValue(player, EBGUAttrFloat.Stamina)
        Dim staMax = BGUFunctionLibraryCS.GetAttrValue(player, EBGUAttrFloat.StaminaMax)

        Dim userMessage = $"生命值 {hp}/{hpMax}
法力值 {mp}/{mpMax}
耐力 {sta}/{staMax}"

        ShowTip("AI 正在总结你的状态...")

        Dim aiResponse = Await AskAIAsync("qwen2:latest", "http://localhost:11434/api/chat",
                         "你是游戏助手，你负责总结玩家状态值的情况。状态值的格式为：名称 当前数值/最大值。你需要对每个状态值做出总结。",
                         userMessage)

        Console.WriteLine(aiResponse)
        ' 消息框太小了，放不下这么多字
        Await InputBoxAsync(aiResponse, "玩家状态 (AI 评价)")
    End Sub

    Private Sub AITest_Load(sender As Object, e As EventArgs) Handles Me.Load
        AKey = My.Computer.Keyboard.Keys(UnrealEngine.InputCore.EKeys.A)
        Console.WriteLine("Press Ctrl+A to summarize status with qwen2 on local ollama server")
    End Sub

    Private Sub AKey_KeyDown(sender As Object, e As KeyEventArgs) Handles AKey.KeyDown
        If e.Modifiers.HasFlag(ModifierKeys.Control) Then
            PrintPlayerInfoAI()
        End If
    End Sub
End Class

Public Module AIHelper

    Async Function AskAIAsync(model As String, requestUrl As String, systemMessage As String, userMessage As String) As Task(Of String)
        Dim request As New With {
            model,
            .messages = {
                New With {
                    .role = "system",
                    .content = systemMessage
                },
                New With {
                    .role = "user",
                    .content = userMessage
                }
            },
            .stream = False
        }

        Dim json = JsonConvert.SerializeObject(request)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Dim response As HttpResponseMessage = Await My.Computer.Network.PostAsync(requestUrl, content)

        If response.IsSuccessStatusCode Then
            Dim responseBody = Await response.Content.ReadAsStringAsync()
            Dim responseObject = JsonConvert.DeserializeObject(Of ChatResponse)(responseBody)

            Return responseObject.message.content
        Else
            Return "Error: " & response.StatusCode
        End If
    End Function

End Module

Public Class ChatResponse
    Public Property model As String
    Public Property created_at As String
    Public Property message As Message
    Public Property done As Boolean
    Public Property total_duration As Long
    Public Property load_duration As Long
    Public Property prompt_eval_count As Integer
    Public Property prompt_eval_duration As Long
    Public Property eval_count As Integer
    Public Property eval_duration As Long
End Class

Public Class Message
    Public Property role As String
    Public Property content As String
End Class
