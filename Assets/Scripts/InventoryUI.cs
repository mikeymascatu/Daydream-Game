using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public GameObject slotPrefab;  // prefab with InventorySlotUI
    public Transform slotsParent;
    List<NewInventorySlot> slotUIs = new List<NewInventorySlot>();

    void Start()
    {
        BuildSlots();
        Refresh();
    }

    void BuildSlots()
    {
        if (slotsParent == null || slotPrefab == null) return;
        // create maxSlots slots
        for (int i = 0; i < playerInventory.maxSlots; i++)
        {
            var go = Instantiate(slotPrefab, slotsParent);
            var slot = go.GetComponent<NewInventorySlot>();
            slot.slotIndex = i;
            slot.playerInventory = playerInventory;
            slotUIs.Add(slot);
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < slotUIs.Count; i++)
            slotUIs[i].UpdateSlot();
    }

    void Update()
    {
        // refresh every frame if you want (or better: subscribe to inventory change events)
        Refresh();
    }
}