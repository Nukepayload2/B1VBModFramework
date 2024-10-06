# B1VBModFramework

## B1VBModFramework

《黑神话·悟空》的 VB 模组框架。

目前处于早期开发阶段。

依赖 [B1CSharpLoader 0.0.6](https://github.com/czastack/B1CSharpLoader)。

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

## How to compile the library
- Clone [B1CSharpLoader](https://github.com/czastack/B1CSharpLoader).
- Edit `Nukepayload2.GameMods.BlackMythWukong.vbproj`.
- Update value of `<B1CSharpLoaderPath>` to the root path of `B1CSharpLoader`, make sure that the path separator is the last character.

## How to use it to make MODs
WIP.

We'll publish a VB project template and then complete this part.

You can try to edit and run [Examples](Examples/BasicModTest.sln) before the project template is ready. `BasicModTest.vbproj` and `My Project\launchSettings.json` have hard coded paths to modify.
