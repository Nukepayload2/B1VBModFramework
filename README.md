# B1VBModFramework

## B1VBModFramework

�����񻰡���ա��� VB ģ���ܡ�

Ŀǰ�������ڿ����׶Ρ�

���� [B1CSharpLoader 0.0.6/0.0.9 (������ 0.0.8)](https://github.com/czastack/B1CSharpLoader)��

## ����
- ���ٷ���ȫ�ֶ���
    - ��ǰģ���ʵ��
    - World
    - Player (Controller, Pawn)
- My ��չ
    - My.World �������ȫ�ֶ����Լ������ʲ��ļ�
    - My.Player �������ȫ�ֶ����¼����ϡ���ҵ���ֵ״̬�������ֱ�ŵ�����͸���״̬���Լ������õ�����״̬��
    - My.Computer ������̡���ꡢ�ֱ��İ������롢�������磬�Լ�������Ϸ���õ�������
    - My.Window ������Ϸ���ڵ�״̬�͵������Լ��ڴ����ϵ����˵���
- VB �ľ��佻�� API
    - MsgBox ��Ϣ��
    - InputBox �����
    - Print ��Ϊ�� ShowTip�������������
- Windows Forms �ĳ�ʼ���߼�
    - InitializeComponents
    - Load �� Unload �¼�
- WPF �� API
    - Dispatcher
        - BeginInvoke
        - Invoke
        - ������Ϸ���̣߳���֧�� InvokeAsync
    - DispatcherTimer: ����ʱ��������ָ�� Dispatcher
    - ��Ϸ���߳��ϵ�ͬ�������ģ���������Ϸ�߳������� `Await` ֮���ܻص�ԭ�����߳��ϡ�
    - VisualTreeHelper: ��ȡ��Ԫ�غ��ϼ�Ԫ��

## �ƻ��еĹ���
Խ�ǲ����ױ���Ϸ����Ӱ��Ĺ��ܣ�Խ���ȴ���

- [P1] (������) My.Log ������׵���־����
- [P1] (�ڶ��ֵ�����) ����ʱ AI ����: ���ô�����ģ��
- [P1] (������) ���ʱ AI ���ܣ�API �ٲ飨���ṩͨ�õ� C#/VB API ���ҹ��ߣ���Ϸ�Ĵ������Լ���취��
- [P2] ��Ŀģ�� (dotnet new)
- [P2] �������¼����ܹ��Զ�ȡ�������ѻ��ն�����¼��������
- [P3] ��װ��Ϸ�� UI �ؼ�������ʹ����� XAML ��׼
- [P3] ��Ϸ�ڸ��Ǵ���
- [P3] ����ȫ���¼������� API Hook ʵ��

## ��α����
- ��¡ [B1CSharpLoader](https://github.com/czastack/B1CSharpLoader)
- �༭ `Nukepayload2.GameMods.BlackMythWukong.vbproj`
- ���� `<B1CSharpLoaderPath>` ��ֵΪ `B1CSharpLoader` �ĸ�·����ȷ��·���ָ��������һ���ַ���

## ���ʹ��������ģ��
����׼���ⲿ�ֵĽ̡̳�

���ǽ��ᷢ��һ�� VB ��Ŀģ��Ȼ������ⲿ�����ݡ��ڴ�֮ǰ����Գ��Ա༭������[ʾ��](Examples/BasicModTest.sln)��`BasicModTest.vbproj` �� `My Project\launchSettings.json` ����Ӳ�����·����Ҫ�޸ġ�

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
