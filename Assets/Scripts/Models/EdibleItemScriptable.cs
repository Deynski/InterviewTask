using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EdibleItemScriptable : ItemScriptable, IDestroyableItem, IItemAction
{
    [SerializeField]
    private List<ModifierData> m_ModifiersData = new List<ModifierData>();

    public string ActionName => "Use";

    [field: SerializeField]
    public AudioClip ActionSFX { get; private set; }

    public bool PerformAction(GameObject i_Character , List<ItemParameterStruct> i_ItemState = null)
    {
        foreach (ModifierData data in m_ModifiersData)
        {
            data.StatModifier.AffectCharacter(i_Character, data.Value);
        }
        return true;
    }
}


public interface IDestroyableItem
{

}

public interface IItemAction
{
    public string ActionName { get; }
    public AudioClip ActionSFX { get; }
    bool PerformAction(GameObject character, List<ItemParameterStruct> itemState);
}

[Serializable]
public class ModifierData
{
    public StatModifierScriptable StatModifier;
    public float Value;
}


