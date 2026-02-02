using UnityEngine;
using Game.Runtime.Core;
using System.Collections;
using Game.Runtime.Data;

public class BoxingController : MonoBehaviour
{
    [Header("Sprites (toggle)")]
    [SerializeField] private GameObject maskOffSprite; // 默认显示
    [SerializeField] private GameObject maskOnSprite; // 面具状态显示

    [Header("Interact target")]
    [SerializeField] private Transform interactTarget; // 戴上面具后允许交互


    private void Awake()
    {
        AudioManager.Instance.PlaySFX(AudioName.BoxingCongrat);
    }

    private void OnEnable()
    {
        EventHandler.MaskStateChangedEvent += OnMaskChanged;

        // 初始化
        if (MaskManager.Instance != null)
            OnMaskChanged(MaskManager.Instance.MaskState);
        else
            ApplyMaskState(MaskState.MaskOff); // 没有 MaskManager 时按默认处理
    }

    private void OnDisable()
    {
        EventHandler.MaskStateChangedEvent -= OnMaskChanged;
    }

    private void OnMaskChanged(MaskState state)
    {
        ApplyMaskState(state);
    }

    private void ApplyMaskState(MaskState state)
    {
        bool maskOn = (state == MaskState.MaskOn);

        if (maskOffSprite != null) maskOffSprite.SetActive(!maskOn);
        if (maskOnSprite != null) maskOnSprite.SetActive(maskOn);

        if(maskOn){AudioManager.Instance.PlaySFX(AudioName.BoxingFight);}
        if (interactTarget != null) interactTarget.gameObject.SetActive(maskOn);

    }
}
