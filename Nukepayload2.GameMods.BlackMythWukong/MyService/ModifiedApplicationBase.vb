Namespace MyService
    Public Class ModifiedApplicationBase
        Public ReadOnly Property ExecutablePath As String
            Get
                ' Some diagnostics APIs were trimmed. The hard coded file name is a workaround.
                Return IO.Path.Combine(BaseDirectory, "b1-Win64-Shipping.exe")
            End Get
        End Property

        Public ReadOnly Property BaseDirectory As String
            Get
                Return If(AppDomain.CurrentDomain.BaseDirectory, Environment.CurrentDirectory)
            End Get
        End Property

        Public ReadOnly Property CommonAppDataPath As String
            Get
                Return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            End Get
        End Property

        Public ReadOnly Property UserAppDataPath As String
            Get
                Return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            End Get
        End Property
    End Class

End Namespace
