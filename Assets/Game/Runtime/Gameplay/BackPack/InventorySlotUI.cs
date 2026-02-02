using Game.Runtime.Core;
using Game.Runtime.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;

    [Header("Inspect Panel (pre-placed in scene)")]
    [SerializeField] private InspectPanel inspectPanel;

    [Header("Doll special inspect")]
    [SerializeField] private ItemSO dollItem;
    [SerializeField] private Sprite dollHorrorSprite;
    [TextArea]
    [SerializeField] private string dollHorrorDescription;

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
        if (inspectPanel == null) return;

        bool isMaskOn = MaskManager.Instance != null && MaskManager.Instance.IsMaskOn;

        Sprite spriteToShow = entry.icon;
        string descToShow = entry.description;

        if (isMaskOn && entry.key == "Doll")
        {
            if (dollHorrorSprite != null) spriteToShow = dollHorrorSprite;
            if (!string.IsNullOrEmpty(dollHorrorDescription)) descToShow = dollHorrorDescription;

            EventHandler.CallFragmentCollectedEvent("fragment_doll");
            EventHandler.CallAnomalyCompletedEvent("Doll");
        }

        inspectPanel.Show(new InspectData(spriteToShow, descToShow));
    }
}
