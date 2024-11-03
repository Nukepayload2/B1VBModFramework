Imports Nukepayload2.GameMods.BlackMythWukong.MyService

Namespace My
    ' 这个类最好是用源生成器写出来，并且把固定内容做成类库。这样方便做版本管理。

    ''' <summary>
    ''' 管理单实例对象，并且提供代理来方便调用常用的游戏 API。<br/>
    ''' Manages singletons. Provides API proxies to invoke frequently used game APIs.
    ''' </summary>
    Module MyB1ModExtension
        ' 仿照 Windows Forms My Extension 设计。

        ''' <summary>
        ''' 当前模组的实例。<br/>
        ''' Gets instance of the current MOD.
        ''' </summary>
        Public Property [Mod] As BasicMod1
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
        ''' 用于向指定的 TraceListeners 输出日志。<br/>
        ''' Enables logging to configured TraceListeners.
        ''' </summary>
        Public ReadOnly Property Log As New Logging.Log
    End Module

End Namespace
