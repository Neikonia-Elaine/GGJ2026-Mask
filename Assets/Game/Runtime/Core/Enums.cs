namespace Game.Runtime.Core
{
    public enum GamePhase
    {
        GameOpening, //开场剧情
        GameTitle, // 游戏标题界面
        MonitorRoom, // 监控室场景
        Gameplay, // 正常游戏中（可移动、可交互）
        ClawMachineGame, //在抓娃娃游戏中，隐藏玩家
        MiniGame,
        FoodTruck, // 在美食中，隐藏玩家，但允许使用能力
        GameOver // 游戏结束
    }

    public enum MaskState
    {
        MaskOn, // 戴上面具（异常画面）
        MaskOff // 摘下面具（恢复正常）
    }
}