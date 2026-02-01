using Game.Runtime.Core;
using Game.Runtime.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;

    [Header("Doll special inspect")]
    [SerializeField] private ItemSO dollItem;
    [SerializeField] private Sprite dollHorrorSprite;      // MaskOn 查看时显示
    [TextArea]
    [SerializeField] private string dollHorrorDescription; // MaskOn 查看时描述

    private InvEntry entry;

    public void Clear()
    {
        entry = null;

        if (icon == null) return;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void SetEntry(InvEntry e)
    {
        entry = e;

        if (icon == null) return;
        icon.sprite = (e != null) ? e.icon : null;
        icon.enabled = (e != null && e.icon != null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (entry == null) return;
        if (entry.icon == null) return;

        bool isMaskOn = MaskManager.Instance != null && MaskManager.Instance.IsMaskOn;

        Sprite spriteToShow = entry.icon;
        string descToShow = entry.description;

        // MaskOn 时，查看娃娃显示恐怖娃娃，并收集到碎片
        if (isMaskOn && entry.key == "Doll")
        {
            if (dollHorrorSprite != null) spriteToShow = dollHorrorSprite;
            if (!string.IsNullOrEmpty(dollHorrorDescription)) descToShow = dollHorrorDescription;
            EventHandler.CallFragmentCollectedEvent("fragment_doll");
            EventHandler.CallAnomalyCompletedEvent("Doll");
        }

        UIManager.Instance.Open<InspectPanel>(
            new InspectData(spriteToShow, descToShow)
        );
    }
}
