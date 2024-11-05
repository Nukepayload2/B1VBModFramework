Imports Nukepayload2.GameMods.BlackMythWukong.MyService

Namespace My
    Module MyB1LibraryExtension
        ''' <summary>
        ''' 管理当前游戏的 World 对象。<br/>
        ''' Gets the current World object.
        ''' </summary>
        Public ReadOnly Property World As New WorldProxy
        ''' <summary>
        ''' 当前本地的第一个玩家。<br/>
        ''' Gets the first local player.
        ''' </summary>
        Public ReadOnly Property Player As New PlayerProxy
        ''' <summary>
        ''' 访问当前计算机的设备。<br/>
        ''' Gets the devices of the local computer.
        ''' </summary>
        Public ReadOnly Property Computer As New ComputerProxy
        ''' <summary>
        ''' 获取游戏窗口。<br/>
        ''' Gets the game window.
        ''' </summary>
        Public ReadOnly Property Window As New WindowProxy
        ''' <summary>
        ''' 获取游戏应用程序的信息。<br/>
        ''' Gets the game app info.
        ''' </summary>
        Public ReadOnly Property Application As New ModifiedApplicationBase
        ''' <summary>
        ''' 获取游戏模组的信息。<br/>
        ''' Gets the game MOD info.
        ''' </summary>
        Public ReadOnly Property [Mod] As New ModBase
        ''' <summary>
        ''' 用于向指定的 TraceListeners 输出日志。<br/>
        ''' Enables logging to configured TraceListeners.
        ''' </summary>
        Public ReadOnly Property Log As New Logging.Log
    End Module

End Namespace
