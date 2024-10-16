Imports System.Reflection
Imports b1.Plugins.AkAudio
Imports B1UI.GSUI
Imports Nukepayload2.GameMods.BlackMythWukong.Media

Namespace MyService
    Public Class AudioProxy
        Private ReadOnly _b1UIAssembly As Assembly = GetType(UIMiniGM).Assembly
        Private ReadOnly _gSUIAudioUtilType As Type = _b1UIAssembly.GetType("B1UI.Script.GSUI.Util.GSUIAudioUtil")
        Private ReadOnly _playUISound As Func(Of String, Integer) =
            DirectCast(_gSUIAudioUtilType.GetMethod("PlayUISound", BindingFlags.Static Or BindingFlags.Public).
            CreateDelegate(GetType(Func(Of String, Integer))), Func(Of String, Integer))
        Private ReadOnly _loadBank As Func(Of String, UAkAudioEvent) =
            DirectCast(_gSUIAudioUtilType.GetMethod("LoadBank", BindingFlags.Static Or BindingFlags.Public).
            CreateDelegate(GetType(Func(Of String, UAkAudioEvent))), Func(Of String, UAkAudioEvent))
        Private ReadOnly _playUISoundWithAkEvent As PlayUISoundWithAkEvent =
            DirectCast(_gSUIAudioUtilType.GetMethod("PlayUISoundWithAkEvent", BindingFlags.Static Or BindingFlags.Public).
            CreateDelegate(GetType(PlayUISoundWithAkEvent)), PlayUISoundWithAkEvent)
        Private Delegate Function PlayUISoundWithAkEvent(AkEvent As UAkAudioEvent, CallbackTypeList As List(Of EAkCallbackType), OnAkPostEventCallback As FOnAkPostEventCallback) As Integer

        ''' <summary>
        ''' 播放指定的系统声音。
        ''' </summary>
        ''' <param name="systemSoundName">自定义的系统声音名称</param>
        Public Sub PlaySystemSound(systemSoundName As String)
            _playUISound(systemSoundName)
        End Sub

        ''' <summary>
        ''' 播放指定的系统声音。
        ''' </summary>
        ''' <param name="systemSound">预设的系统声音</param>
        Public Sub PlaySystemSound(systemSound As SystemSound)
            _playUISound(systemSound.ToString)
        End Sub

        ''' <summary>
        ''' 播放指定资源的声音。
        ''' </summary>
        ''' <param name="resourceUri">
        ''' 例如：/Game/00Main/Audio/SFX/UI/HUD/EVT_ui_hud_hint_itembig_disappear.EVT_ui_hud_hint_itembig_disappear
        ''' </param>
        Public Sub PlayBank(resourceUri As String)
            Dim bankName = $"AkAudioEvent'{resourceUri}'"
            _playUISoundWithAkEvent(
                _loadBank(bankName),
                New List(Of EAkCallbackType), Nothing)
        End Sub
    End Class

End Namespace
