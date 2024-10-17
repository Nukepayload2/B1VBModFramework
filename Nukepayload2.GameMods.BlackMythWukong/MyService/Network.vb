Imports System.Net.Http

Namespace MyService
    Public Class Network
        Private Class InternalWinHttpHandler
            Inherits WinHttpHandler

            Public Overloads Function SendAsync(request As HttpRequestMessage, cancellationToken As Threading.CancellationToken) As Task(Of HttpResponseMessage)
                Return MyBase.SendAsync(request, cancellationToken)
            End Function
        End Class

        Private ReadOnly _http As New InternalWinHttpHandler
        Private ReadOnly _httpClient As New HttpClient(_http, True)

        Public Async Function GetAsync(requestUri As String) As Task(Of HttpResponseMessage)
            Return Await _httpClient.GetAsync(requestUri)
        End Function

        Public Async Function PostAsync(requestUrl As String, content As HttpContent) As Task(Of HttpResponseMessage)
            Return Await _httpClient.PostAsync(requestUrl, content)
        End Function
    End Class
End Namespace
