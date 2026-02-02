using Game.Runtime.Core;
using UnityEngine;
public class BalloonShow : MonoBehaviour
{
   public GameObject balloon;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        EventHandler.MaskStateChangedEvent += OnMaskChanged;

        // 获取面具状态初始化
        if (MaskManager.Instance != null)
            OnMaskChanged(MaskManager.Instance.MaskState);
    }

    private void OnDisable()
    {
        EventHandler.MaskStateChangedEvent -= OnMaskChanged;
    }

    private void OnMaskChanged(MaskState state)
    {
        if (balloon == null) return;
        if (state == MaskState.MaskOn)
        {
            balloon.SetActive(true);
        }
        else
        {
            balloon.SetActive(false);
        }

    }
}