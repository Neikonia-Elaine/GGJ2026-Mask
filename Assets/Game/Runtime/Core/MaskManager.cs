using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Core
{
    /// <summary>
    /// MaskManager：全局“面具状态”管理器。
    /// 
    /// 作用：
    /// 1) 维护当前 MaskState（MaskOn / MaskOff），作为“异常视角”的唯一真相来源；
    /// 2) 当面具状态变化时，通过 EventHandler.CallMaskStateChangedEvent 广播事件，
    ///    让场景内所有订阅者（娃娃机/拳击机/餐车/气球贩子/音效/贴图等）自行切换表现；
    /// 3) 使用 Singleton + DontDestroyOnLoad，保证跨场景切换时状态与事件入口保持一致。
    /// 
    /// 使用方式：
    /// - 输入层（键盘/手柄）调用 ToggleMask() 或 SetMaskState(...)；
    /// - 任何需要判断当前是否戴面具的脚本，读取 MaskManager.Instance.IsMaskOn。
    /// </summary>
   
    public class MaskManager : MonoBehaviour
    {
        /// <summary>
        /// 全局单例实例（场景中只应存在一个 MaskManager）。
        /// </summary>
        public static MaskManager Instance { get; private set; }

        /// <summary>
        /// 当前面具状态（MaskOff 为默认）。
        /// </summary>
        [SerializeField] private MaskState maskState = MaskState.MaskOff;

        /// <summary>
        /// 获取当前面具状态（MaskOn / MaskOff）。
        /// </summary>
        public MaskState MaskState => maskState;

        /// <summary>
        /// 当前是否处于戴面具状态 -> 语法糖。
        /// - true：异常视角启用
        /// - false：正常视角
        /// </summary>
        public bool IsMaskOn => maskState == MaskState.MaskOn;

        /// <summary>
        /// Unity 生命周期：Awake。
        /// - 初始化单例 Instance；
        /// - 保证跨场景不销毁；
        /// - 若重复生成，则销毁新对象避免出现多个管理器。
        /// </summary>
        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 设置面具状态（推荐对外暴露的“唯一入口”之一）。
        /// 
        /// 行为：
        /// - 若状态没变，则不做任何事；
        /// - 若状态变化，则更新 maskState 并广播 MaskStateChangedEvent；
        /// 
        /// 典型调用场景：
        /// - 玩家按键戴/摘面具；
        /// - 演出/结局强制摘下面具；
        /// - 切换场景时重置为 MaskOff（如果你需要）。
        /// </summary>
        public void SetMaskState(MaskState state)
        {
            if (maskState == state) return;
            maskState = state;
            EventHandler.CallMaskStateChangedEvent(maskState);
        }

        /// <summary>
        /// 调用切换面具状态（MaskOff <-> MaskOn）。
        /// 
        /// 行为：
        /// - 当前 MaskOff -> 变为 MaskOn；
        /// - 当前 MaskOn  -> 变为 MaskOff；
        /// - 内部会调用 SetMaskState(...)，并在状态变化时广播事件。
        /// 
        /// 典型调用场景：
        /// - 绑定到输入（例如按 M / 右键 / 手柄按键）实现“一键戴/摘面具”。
        /// </summary>
        public void ToggleMask()
        {
            SetMaskState(IsMaskOn ? MaskState.MaskOff : MaskState.MaskOn);
        }
    }
}

