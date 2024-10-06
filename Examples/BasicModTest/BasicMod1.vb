﻿Partial Class BasicMod1

    ' 处理当前模组的生命周期事件

    Private Sub InitializeComponents()
        With Components
            .Add(New StatusDisplay)
            .Add(New MenuAndMessageBoxTests)
            .Add(New GamepadCyberwaresTest)
        End With
    End Sub

    Private Sub BasicMod1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Console.WriteLine($"Loaded components {String.Join(", ",
             From comp In Components Select comp.GetType.FullName)}")
    End Sub

End Class