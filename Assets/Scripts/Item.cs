using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SlotTag { None, Head, Chest, Legs, Feet}

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item: ScriptableObject
{
    public Sprite sprite;
    public string displayName;
    public float weight = 1f;
    public float weaponDamage = 1f;
    public GameObject worldPrefab;
}

