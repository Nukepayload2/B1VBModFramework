Imports System.Net
Imports System.Reflection
Imports b1
Imports BtlShare

Class AITest
    Inherits ModComponentBase

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

        Try
            Await MsgBoxAsync(String.Join(vbCrLf,
                      From item In GetType(HttpWebRequest).GetConstructors(BindingFlags.NonPublic)
                      Select String.Join(",", From p In item.GetParameters Select p.ParameterType.FullName)))
            WebRequest.RegisterPrefix("http", DirectCast(Activator.CreateInstance(GetType(HttpWebRequest).Assembly.GetType("System.Net.HttpRequestCreator")), IWebRequestCreate))
            Dim test1 = WebRequest.CreateHttp("http://localhost:11434/api/chat")
            ShowTip("WebRequest.CreateHttp OK...")
        Catch ex As Exception
            ShowTip("WebRequest.CreateHttp ERR...")
            Console.WriteLine(ex)
            Return
        End Try

        Dim aiResponse = Await LegacyAskAIAsync("qwen2:latest", "http://localhost:11434/api/chat",
                         "你是游戏助手，你负责总结玩家状态值的情况。状态值的格式为：名称 当前数值/最大值。你需要对每个状态值做出总结。",
                         userMessage)

        Await MsgBoxAsync(aiResponse,, "玩家状态 (AI 评价)")
    End Sub

End Class

Public Module AIHelper

    Async Function AskAIAsync(model As String, requestUrl As String, systemMessage As String, userMessage As String) As Task(Of String)

        'Dim httpClient As New HttpClient(New HttpClientHandler With {.UseProxy = False})

        'Dim request As New With {
        '    model,
        '    .messages = {
        '        New With {
        '            .role = "system",
        '            .content = systemMessage
        '        },
        '        New With {
        '            .role = "user",
        '            .content = userMessage
        '        }
        '    },
        '    .stream = False
        '}

        'Dim json = JsonConvert.SerializeObject(request)
        'Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        'Dim response = Await httpClient.PostAsync(requestUrl, content)

        'If response.IsSuccessStatusCode Then
        '    Dim responseBody = Await response.Content.ReadAsStringAsync()
        '    Dim responseObject = JsonConvert.DeserializeObject(Of ChatResponse)(responseBody)

        '    Return responseObject.message.content
        'Else
        '    Return "Error: " & response.StatusCode
        'End If
    End Function

    Async Function LegacyAskAIAsync(model As String, requestUrl As String, systemMessage As String, userMessage As String) As Task(Of String)
        'Dim webClient As New WebClient()

        'Dim request As New With {
        '    model,
        '    .messages = {
        '        New With {
        '            .role = "system",
        '            .content = systemMessage
        '        },
        '        New With {
        '            .role = "user",
        '            .content = userMessage
        '        }
        '    },
        '    .stream = False
        '}

        'Dim json = JsonConvert.SerializeObject(request)

        'webClient.Headers.Add("Content-Type", "application/json")
        'Dim responseBytes = Await webClient.UploadDataTaskAsync(New Uri(requestUrl, UriKind.Absolute), "POST", Encoding.UTF8.GetBytes(json))

        'Dim response = Encoding.UTF8.GetString(responseBytes)

        'If response.StartsWith("{"c) Then
        '    Dim responseObject = JsonConvert.DeserializeObject(Of ChatResponse)(response)

        '    Return responseObject.message.content
        'Else
        '    Return "Error: " & response
        'End If
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
