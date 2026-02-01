using UnityEngine;

namespace Game.Runtime.Data
{
    [CreateAssetMenu(fileName = "AnomalySO", menuName = "Anomaly")]
    public class AnomalySO : ScriptableObject
    {
        [Header("Identity")]
        public AnomalyName anomalyName = AnomalyName.None; // 建议用 enum，避免字符串拼错

        [Header("Fragment")]
        public Sprite fragmentSprite;
        [TextArea] public string fragmentDescription;

        [Header("Completion")]
        [TextArea]
        public string completionHintText;  // 完成提示文本
        public FragmentName fragmentName = FragmentName.None; // 对应碎片

        [Header("Anomaly Effect")]
        public AudioClip abnormalAudio;    // 戴面具交互：异常音效
    }

    public enum AnomalyName
    {
        None,
        Doll,        // 娃娃机：背包恐怖娃娃
        Boxing,      // 拳击机：争吵/搏斗声
        JigsawPuzzle,   // 寻人启事拼图
        Balloon      // 气球贩子：红->黑 + 玩法触发
    }

    // 收集异常碎片占位符
    public enum FragmentName
    {
        None,
        fragment_doll,
        Fragment_puzzle,
        Fragment3,
        Fragment4
    }
}
