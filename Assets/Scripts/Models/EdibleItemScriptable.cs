using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EdibleItemScriptable : ItemScriptable, IDestroyableItem, IItemAction
{
    [SerializeField]
    private List<ModifierData> m_ModifiersData = new List<ModifierData>();

    public string ActionName => "Consume";

    public AudioClip ActionSFX { get; private set; }

    public bool PerformAction(GameObject i_Character)
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
    bool PerformAction(GameObject character);
}

[Serializable]
public class ModifierData
{
    public StatModifierScriptable StatModifier;
    public float Value;
}


