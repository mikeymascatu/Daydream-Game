using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class NewInventorySlot : MonoBehaviour, IPointerClickHandler
{
    public int slotIndex;
    public PlayerInventory playerInventory;
    public Image sprite;
    public Text countText;

    void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (playerInventory == null || slotIndex < 0 || slotIndex >= playerInventory.items.Count)
        {
            sprite.enabled = false;
            if (countText) countText.text = "";
            return;
        }

        var stack = playerInventory.items[slotIndex];
        if (stack != null && stack.item != null)
        {
            sprite.enabled = true;
            sprite.sprite = stack.item.sprite;
            if (countText) countText.text = stack.count > 1 ? stack.count.ToString() : "";
        }
    }

    // left click to drop one, right click to drop stack
    public void OnPointerClick(PointerEventData eventData)
    {
        if (playerInventory == null) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            playerInventory.DropSlotInFront(slotIndex, 1);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // drop whole stack
            if (slotIndex < playerInventory.items.Count)
            {
                int count = playerInventory.items[slotIndex].count;
                playerInventory.DropSlotInFront(slotIndex, count);
            }
        }
        // after change, ask UI to refresh (you can do this better with events)
        var ui = FindObjectOfType<InventoryUI>();
        if (ui != null) ui.Refresh();
    }
}