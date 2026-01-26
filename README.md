# GGJ2026

Unity Version: Unity 2022.3 LTS(2022.3.62f3)

使用的package：
1. InputSystem
2. Cinemachine
3. TextMeshPro
4. Newtonsoft.Json

## 玩法介绍
todo：

## 项目结构

```
/Assets
  /Game          # 游戏脚本
    /Core          # 核心功能（包括事件、场景、音频、UI管理器、全局设置）
    /Data          # 数据定义，如 ScriptableObject、配置数据
    /Gameplay      # 玩法模块代码
  /GameData      # 具体的数据，excel表格，ScriptableObject 文件等
……
/Packages         # Unity Package 管理
/ProjectSettings  # Unity 项目设置
```
## Game目录说明

C#脚本都放在Game目录下~

### 💡 Core

- 程序集：Game.Runtime.Core.dll
- 放**和游戏玩法没有关系的**核心框架逻辑，如：
  - Singleton: 单例基类
  - UIManager：面板管理器
  - TransitionManager：场景转换管理器
  - EventHandle：消息转发中心
  - Enum：各种枚举
  - Utils：通用工具库
  - DataLoader：数据表读取类


### 📈 Data

- 程序集：Game.Runtime.Data.dll
- 放各种数据类：ScriptableObject 配置，自己定义的数据结构等。
- 注意！！！**Data只能放数据模板！`Game.Runtime.Core`可以引用`Game.Runtime.Data`，`Data`不可以引用其他程序集！**

### 🎮 Gameplay

- 没有封装程序集，普通脚本都在`Assembly-CSharp.dll`里面~
- Gameplay可以自由引用 `Game.Runtime.Core`和`Game.Runtime.Data`
- 放具体玩法逻辑脚本

## 在线试玩链接
Todo
