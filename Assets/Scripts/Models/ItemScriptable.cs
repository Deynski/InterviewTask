
using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemScriptable : ScriptableObject
{
    [field: SerializeField]
    public bool IsStackable { get; set; }

    [field: SerializeField]
    public int ID => GetInstanceID();

    [field: SerializeField]
    public int MaxStackSize { get; set; } = 1;

    [field: SerializeField]
    public int Price { get; set; } = 0;

    [field: SerializeField]
    public int SellAmount { get; set; } = 0;

    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    [field : TextArea]
    public string Description { get; set; }

    [field: SerializeField]
    public Sprite ItemImage { get; set; }

    [field: SerializeField]
    public List<ItemParameterStruct> Parameters { get; set; }
}

[Serializable]

public struct ItemParameterStruct : IEquatable<ItemParameterStruct>
{
    public ItemParameterScriptable ItemParameterScriptable;
    public float Value;

    public bool Equals(ItemParameterStruct other)
    {
        return other.ItemParameterScriptable == ItemParameterScriptable;
    }
}
