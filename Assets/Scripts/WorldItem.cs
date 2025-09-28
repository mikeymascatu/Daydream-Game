using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class WorldItem : MonoBehaviour
{
    public Item item;
    public int amount = 1;
    public bool autoPickup = true; // set true for instant pickup on touch

    void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (autoPickup)
        {
            var inv = other.GetComponent<PlayerInventory>();
            if (inv != null)
            {
                inv.Pickup(item, amount);
                Destroy(gameObject);
            }
        }
    }
}
