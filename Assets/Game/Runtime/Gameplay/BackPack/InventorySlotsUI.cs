using UnityEngine;
using Game.Runtime.Core;
using UnityEngine.UI;

public class InventorySlotsUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventorySlotUI[] slots;

    [Header("Doll special display")]
    [SerializeField] private Sprite dollHorrorSprite; // MaskOn 显示
    [SerializeField] private ItemSO dollItem;         // 普通娃娃 ItemSO

    private void Awake()
    {
        if (slots == null || slots.Length == 0)
            slots = GetComponentsInChildren<InventorySlotUI>(true);
    }

    private void OnEnable()
    {
        Inventory.InventoryChangedEvent += OnInventoryChanged;
        EventHandler.MaskStateChangedEvent += OnMaskChanged;
        Refresh();
    }

    private void OnDisable()
    {
        Inventory.InventoryChangedEvent -= OnInventoryChanged;
        EventHandler.MaskStateChangedEvent -= OnMaskChanged;
    }

    private void OnMaskChanged(MaskState _)
    {
        Refresh();
    }

    private void OnInventoryChanged()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (inventory == null || slots == null) return;

        for (int i = 0; i < slots.Length; i++)
            if (slots[i] != null) slots[i].Clear();

        var list = inventory.Entries;
        int count = Mathf.Min(list.Count, slots.Length);

        bool isMaskOn = MaskManager.Instance != null && MaskManager.Instance.IsMaskOn;

        for (int i = 0; i < count; i++)
        {
            var slot = slots[i];
            if (slot == null) continue;

            var entry = list[i];
            slot.SetEntry(entry);

            // MaskOn 时，把普通娃娃icon替换为恐怖娃娃icon
            if (!isMaskOn) continue;
            if (dollItem == null || dollItem.icon == null) continue;
            if (dollHorrorSprite == null) continue;

            if (entry.key == "Doll")
            {
                ReplaceImageSprite(slot, fromSprite: dollItem.icon, toSprite: dollHorrorSprite);
            }
        }
    }

    // 替换helper
    private void ReplaceImageSprite(InventorySlotUI slot, Sprite fromSprite, Sprite toSprite)
    {
        if (slot == null || fromSprite == null || toSprite == null) return;

        var imgs = slot.GetComponentsInChildren<Image>(true);
        for (int i = 0; i < imgs.Length; i++)
        {
            var img = imgs[i];
            if (img == null) continue;

            if (img.sprite == fromSprite)
            {
                img.sprite = toSprite;
                return;
            }
        }
    }
}
