
<h1 align="center">
TerraAngel
</h1>
<p align="center">
TerraAngel 是一个现代且功能丰富的 Terraria 客户端
</p>
<br>

<h2>
如何安装
</h2>


前提条件：[ git ]( https://git-scm.com/download/win )和[PowerShell 7](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows?view=powershell-7.4)或更高版本以及[ dotnet 8 SDK ]( https://dotnet.microsoft.com/en-us/download/dotnet/8.0)，以及[泰拉瑞亚](https://store.steampowered.com/app/105600/Terraria/)

请务必于安装后重启电脑，这样可以避免发生一些奇奇怪怪的问题。

接下来，跟着教程来，一步一步走:

  1. Shift+右键，打开cmd，或者powershell

  2. 使用这条命令把项目源代码下载(clone)下来: `git clone https://github.com/Duck15/TerraAngel.git --recursive`

  ![image](https://user-images.githubusercontent.com/87276335/182042166-c967bcba-cd52-4372-ad75-3bc5faaac0ea.png)

  3. 下载完成，确保没啥毛病后，运行`cd TerraAngel\scripts`。

  3. 然后运行`fast.ps1 -Start`，程序就开始自动执行编译(build)了。可以喝杯水，刷刷视频，一般几分钟就好了。

  ![image](https://user-images.githubusercontent.com/87276335/182042235-9ce87d19-61ee-4636-b3ab-eee0ccb0e428.png)

  4. 如果一切顺利，没报红报错的话，你就可以在 `src/TerraAngel/Terraria/bin/Release/net8.0/` 这个文件夹里找到 TerraAngel 了。

  ![image](https://user-images.githubusercontent.com/87276335/182298612-c9aa34a2-9df7-4047-9a4f-a465c95419a1.png)

  ![image](https://user-images.githubusercontent.com/87276335/182298616-e9e2299e-611c-4b7d-823e-b4d6ff828c42.png)

<h2>
如何更新
</h2>
运行 `fast.ps1 -Update` 就好了

<h2>
如何开发
</h2>

如果你编译过了TerraAngel，那么Terraria就会反编译出源代码，然后放在`src/TerraAngel/Terraria`

运行`fast.ps1 -Diff`来添加你自己的修改

<h2>
功能特色
</h2>

<h3>
对开发者有用的特性
</h3>

- 快速查询
    - 查询玩家信息
    
    ![image](https://user-images.githubusercontent.com/87276335/227608993-092563ba-64f2-4102-9cbe-1c3723bf8e68.png)
    - 查询NPC信息
    
    ![image](https://user-images.githubusercontent.com/87276335/227608567-45571da7-b75a-4057-8fa8-a7501bcad51f.png)
    - 查询弹幕信息
    
    ![image](https://user-images.githubusercontent.com/87276335/227608900-8a275a82-ee30-4352-b692-8d929bc270bf.png)
    - 查询物品信息
    
    ![image](https://user-images.githubusercontent.com/87276335/227608459-e5c5bd79-1684-419b-84dd-a5d898b5e3c6.png)
    
- 自由镜头: 允许你随意移动你的视角，但是不移动角色本身
- 绘制小工具: 助你方便的看到贴图后面的实际情况
   - 显示玩家判定框
   - 显示玩家手持物品
   - 显示玩家背包
   - 显示NPC判定框，和它的延迟补偿
   - 显示弹幕判定框
   - 显示区块边界
   - 关闭沙漠尘埃效果
   - 关闭血液特效
   - 显示物品的详细信息

   ![image](https://user-images.githubusercontent.com/87276335/197304559-292de6a7-bed1-4cc9-a452-89d70e890981.png)
- 交互式 C # 执行引擎 (aka [REPL](https://en.wikipedia.org/wiki/Read%E2%80%93eval%E2%80%93print_loop))
  - 支持自动补全
- 网络调试
  - 记录发送和接收的日志
  - 发送数据包时的堆栈跟踪信息
  - 支持使用自定义的值来发送消息，并生成 NetMessage.SendData 的调用
- 关闭物块特效
- 支持多架构CPU (x64 and x86)
- 可以看看 [PLUGINS.md](/PLUGINS.md) 来了解有关插件的信息

<h3>
其他有用的小特性
</h3>

- 完全重写的聊天界面
![Terraria_1660961693](https://user-images.githubusercontent.com/87276335/185725363-591a1d7b-a264-4a46-bfb2-96578c8ad6a3.gif)
- 完全重写的多人服务器选择界面
- 上帝模式
- 无视亮度（亮度可调）
- 穿墙模式（同时不受重力限制）
- 物品浏览器
- 地图全开
- 锁血，锁蓝，解锁仆从限制
- 任意难度人物的旅行菜单
- 清除npc（全莎了）
- 弹幕预测
- 支持个性化UI界面
- 世界编辑
  - 物块刷子
    - 基本物块操作
    - 基本液体操作
  - 支持复制黏贴世界的某个部分
- 重写世界物块加载方式 (可以让游戏省一半内存)
- 偷看其他玩家背包
- 直接对地图截图
- 许多对原版的奇奇怪怪的小bug修复
- 神秘性能提升
- 可以更改FPS上限(60->???)

<h2>
未来计划的功能
</h2>

- 某些神秘优化
- UHHHHHHHH

<h2>
系统要求
</h2>

因为TerraAngel 使用*FNA*替代了*XNA*，所以导致兼容性有点变化

- Windows 7+
- OpenGL 3.0+ or D3D11

<h2>
给项目作贡献
</h2>

  1. Fork本项目到你的Github账户下
  2. 在你自己fork过去的项目里，作出你的更改
  3. 使用拉取请求（pr）到原来的项目
  4. 原项目作者会看看你交上来个啥东西，如果还行就合并到原项目

我们欢迎各种各样的贡献，比如代码改进，bug修复，或者一些新特性

<h2>
有疑问？
</h2>

开个issue就好。
