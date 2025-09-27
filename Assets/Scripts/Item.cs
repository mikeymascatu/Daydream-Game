using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SlotTag { None, Head, Chest, Legs, Feet}

[CreateAssestMenu(menuName = "Scriptable Objects/Item")]
public class Item: ScriptableObject
{
    public Sprite sprite;
    public SlotTag itemtag;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
}

