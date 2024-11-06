# B1VBModFramework

## B1VBModFramework

�����񻰡���ա��� VB ģ���ܡ�

Ŀǰ�������ڿ����׶Ρ��ݲ�ȷ��Դ������ԺͶ����Ƽ����ԡ�

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
    - My.Log ������־���ܡ�ʹ�ü򵥵Ĵ��뼴�ɽ���־д�벻ͬ��λ�ã��������ļ����ӡ���־�����ļ�λ�õ����á�
- VB �ľ��佻�� API
    - MsgBox ��Ϣ��
    - InputBox �����
    - Print ��Ϊ�� ShowTip�������������
- ������ Windows Forms �� WPF �ĳ�ʼ���߼�
    - InitializeComponents
    - Initialized��Load �� Unload �¼�
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

At present, we are in the Alpha stage of development. It's important to note that we cannot currently guarantee source compatibility and binary compatibility.

Requires [B1CSharpLoader 0.0.6/0.0.9](https://github.com/czastack/B1CSharpLoader)

## Features

- **Quick Access to Global Objects**
    - Instance of the current module
    - World
    - Player (Controller, Pawn)

- **My Extension**
    - `My.World` for accessing related global objects and loading asset files.
    - `My.Player` for accessing related global objects, event collections, player's numerical states, gains with numeric IDs, negative statuses, and debug-specific statuses.
    - `My.Computer` for handling keyboard, mouse, and controller input keys, network access, and playing game internal sounds.
    - `My.Window` for managing the game window's state and navigation, as well as popping up menus on the window.
    - `My.Log` for handling logging functionality. Logs can be written to different trace listeners with settings like file appending, log levels, and file locations.

- **VB Classic Interaction API**
    - `MsgBox` for displaying dialog boxes
    - `InputBox` for input dialogs
    - `Print` replaced by `ShowTip` to avoid ambiguity

- **Initialization Logic Similar to Windows Forms and WPF**
    - `InitializeComponents`
    - `Initialized`, `Load`, and `Unload` events

- **WPF API**
    - Dispatcher:
        - BeginInvoke
        - Invoke
        - Supports InvokeAsync on the game's main thread
    - DispatcherTimer: Run timers in a specified Dispatcher.
    - Sync context on the game's main thread that allows returning to the original thread after using `Await`.
    - VisualTreeHelper for getting child elements and parent elements.

## How to compile the library
- Clone [B1CSharpLoader](https://github.com/czastack/B1CSharpLoader).
- Edit `Nukepayload2.GameMods.BlackMythWukong.vbproj`.
- Update value of `<B1CSharpLoaderPath>` to the root path of `B1CSharpLoader`, make sure that the path separator is the last character.

## How to use it to make MODs
WIP.

We'll publish a VB project template and then complete this part.

You can try to edit and run [Examples](Examples/BasicModTest.sln) before the project template is ready. `BasicModTest.vbproj` and `My Project\launchSettings.json` have hard coded paths to modify.
