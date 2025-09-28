using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ItemStack
{
    public Item item;
    public int count;
    public ItemStack(Item i, int c) { item = i; count = c; }
}

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public int maxSlots = 10;
    public List<ItemStack> items = new List<ItemStack>();

    [Header("Gravity settings")]
    public Rigidbody2D rb;
    public float baseGravityScale = 1f;    // player's gravity with no items
    public float gravityPerWeight = 0.15f; // multiplier: totalWeight * gravityPerWeight
    public float minGravity = 0.2f;
    public float maxGravity = 10f;
    public bool smoothGravity = true;
    public float gravityLerpSpeed = 6f;

    float targetGravity;
    public float totalDamage = 1f;

    [SerializeField] AttackSystem damage;

    void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        RecalculateGravity();
        //GetTotalDamage();
    }

    void Update()
    {
        // smooth transition for nicer feel
        if (smoothGravity && rb != null)
        {
            rb.gravityScale = Mathf.Lerp(rb.gravityScale, targetGravity, Time.deltaTime * gravityLerpSpeed);
        }
        else if (rb != null)
        {
            rb.gravityScale = targetGravity;
        }
    }

    public bool Pickup(Item itemToAdd, int amount = 1)
    {
        if (itemToAdd == null) return false;

        // try to stack
        var stack = items.Find(s => s.item == itemToAdd);
        if (stack != null)
        {
            stack.count += amount;
            RecalculateGravity();
            //GetTotalDamage();
            return true;
        }

        // add new if there's a free slot
        if (items.Count < maxSlots)
        {
            items.Add(new ItemStack(itemToAdd, amount));
            RecalculateGravity();
            //GetTotalDamage();
            return true;
        }

        // inventory full
        Debug.Log("Inventory full");
        return false;
    }

    public bool DropAtSlot(int slotIndex, int dropAmount = 1, Vector2 dropPosition = default)
    {
        if (slotIndex < 0 || slotIndex >= items.Count) return false;

        var stack = items[slotIndex];
        if (dropAmount <= 0 || stack == null) return false;
        if (dropAmount > stack.count) dropAmount = stack.count;

        // instantiate world prefab (fallback: use a simple placeholder)
        if (stack.item.worldPrefab != null)
        {
            for (int i = 0; i < dropAmount; i++)
            {
                var go = Instantiate(stack.item.worldPrefab, (Vector3)dropPosition + (Vector3)Random.insideUnitCircle * 0.2f, Quaternion.identity);
                // ensure the world drop has a Rigidbody2D so it falls naturally
                if (go.GetComponent<Rigidbody2D>() == null)
                    go.AddComponent<Rigidbody2D>().gravityScale = 1f;
            }
        }
        else
        {
            Debug.LogWarning("No worldPrefab set for item " + stack.item.displayName);
        }

        stack.count -= dropAmount;
        if (stack.count <= 0) items.RemoveAt(slotIndex);

        RecalculateGravity();
        //GetTotalDamage();
        return true;
    }
    
    public void RecalculateGravity()
    {
        float totalWeight = 0f;
        foreach (var s in items)
            totalWeight += (s.item != null ? s.item.weight * s.count : 0f);

        targetGravity = Mathf.Clamp(baseGravityScale + totalWeight * gravityPerWeight, minGravity, maxGravity);
    }
    /*
    public int GetTotalDamage(){
    int total = 0;
    foreach (var stack in items){
        if (stack.item != null)
            total += stack.item.weaponDamage * stack.count;
    }
        return total;
    }
    */
    


    // helper: drop selected slot in front of player
    public bool DropSlotInFront(int slotIndex, int dropAmount = 1, float distance = 0.8f)
    {
        Vector2 dropPos = (Vector2)transform.position + Vector2.right * (transform.localScale.x >= 0 ? distance : -distance);
        return DropAtSlot(slotIndex, dropAmount, dropPos);
    }
}