using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup canavsGroupe { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        canavsGroup = GetComponent<canavsGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item item, inventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        itemIcon.sprite = item.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryItem.Singleton.SetCarriedItem(this);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
