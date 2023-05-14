using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipabbleItemScriptable : ItemScriptable, IDestroyableItem, IItemAction
{
    public string ActionName => "Equip";

    [field: SerializeField]
    public SO_BodyPart BodyPart { get; set; }

    [field: SerializeField]
    public EClothesType ClothesType;

    [field: SerializeField]
    public AudioClip ActionSFX { get; private set; }

    public bool PerformAction(GameObject character, List<ItemParameterStruct> itemState = null)
    {
        AgentEquippable equipSystem = character.GetComponent<AgentEquippable>();

        if(equipSystem != null)
        {
            equipSystem.SetEquippable(this, itemState == null ? Parameters : itemState);
            Debug.Log(this);
            return true;
        }

        return false;
    }

    
   
}
[Serializable]
public enum EClothesType
{
    Torso,
    Pants
}




