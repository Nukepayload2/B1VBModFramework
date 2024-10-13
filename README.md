# B1VBModFramework

## B1VBModFramework

《黑神话·悟空》的 VB 模组框架。

目前处于早期开发阶段。

依赖 [B1CSharpLoader 0.0.6/0.0.9 (不兼容 0.0.8)](https://github.com/czastack/B1CSharpLoader)。

## 功能
- 快速访问全局对象
    - 当前模组的实例
    - World
    - Player (Controller, Pawn)
- My 扩展
    - My.World 访问相关全局对象，以及加载资产文件
    - My.Player 访问相关全局对象、事件集合、玩家的数值状态、带数字编号的增益和负面状态，以及调试用的特殊状态。
    - My.Computer 处理键盘、鼠标、手柄的按键输入，以及播放游戏内置的声音。
    - My.Window 处理游戏窗口的状态和导航，以及在窗口上弹出菜单。
- VB 的经典交互 API
    - MsgBox 消息框
    - InputBox 输入框
    - Print 改为了 ShowTip，避免出现歧义
- Windows Forms 的初始化逻辑
    - InitializeComponents
    - Load 和 Unload 事件
- WPF 的多线程 API
    - Dispatcher
        - BeginInvoke
        - Invoke
        - 对于游戏主线程，还支持 InvokeAsync
    - DispatcherTimer
    - 游戏主线程上的同步上下文

## 如何编译库
- 克隆 [B1CSharpLoader](https://github.com/czastack/B1CSharpLoader)
- 编辑 `Nukepayload2.GameMods.BlackMythWukong.vbproj`
- 更新 `<B1CSharpLoaderPath>` 的值为 `B1CSharpLoader` 的根路径，确保路径分隔符是最后一个字符。

## 如何使用它制作模组
正在准备这部分的教程。

我们将会发布一个 VB 项目模板然后完成这部分内容。在此之前你可以尝试编辑并运行[示例](Examples/BasicModTest.sln)。`BasicModTest.vbproj` 和 `My Project\launchSettings.json` 中有硬编码的路径需要修改。

# B1VBModFramework
A VB MOD framework of Black Myth Wukong.

It's currently in Alpha stage.

Requires [B1CSharpLoader 0.0.6](https://github.com/czastack/B1CSharpLoader)
## Features

### Quick Access to Global Objects
- Instance of the current MOD
- `World`
- `Player` (Controller, Pawn)

### My Extension
- `My.World`: Accesses related global objects and loads asset files.
- `My.Player`: Accesses related global objects, events, player's numerical status, gains and debuffs with numbered IDs, as well as special debugging states.
- `My.Computer`: Handles keyboard, mouse, and gamepad input, as well as in-game sound playback.
- `My.Window`: Manages the game window state and navigation, as well as pop-up menus on the window.

### VB's Classic Interaction API
- `MsgBox` for message boxes
- `InputBox` for input fields
- `Print` replaced with `ShowTip` to avoid ambiguity

### Initialization Logic of Windows Forms
- `InitializeComponents`
- `Load` and `Unload` events

### Multi-threading API in WPF
- **Dispatcher**
  - `BeginInvoke`
  - `Invoke`
  - Supports `InvokeAsync` on the game's main thread for async operations
- **DispatcherTimer**
- **Sync Context on the Game's Main Thread**

## How to compile the library
- Clone [B1CSharpLoader](https://github.com/czastack/B1CSharpLoader).
- Edit `Nukepayload2.GameMods.BlackMythWukong.vbproj`.
- Update value of `<B1CSharpLoaderPath>` to the root path of `B1CSharpLoader`, make sure that the path separator is the last character.

## How to use it to make MODs
WIP.

We'll publish a VB project template and then complete this part.

You can try to edit and run [Examples](Examples/BasicModTest.sln) before the project template is ready. `BasicModTest.vbproj` and `My Project\launchSettings.json` have hard coded paths to modify.
