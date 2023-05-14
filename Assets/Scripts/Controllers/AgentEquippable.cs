using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEquippable : MonoBehaviour
{
    [SerializeField]
    private EquipabbleItemScriptable m_EquippableTorso;

    [SerializeField]
    private EquipabbleItemScriptable m_EquippablePants;

    [SerializeField] 
    private BodyPartsManager m_BodyPartsManager;

    [SerializeField]
    private InventoryScriptable m_InventoryData;

    [SerializeField]
    private List<ItemParameterStruct> m_ParametersToModify, m_ItemCurrentState;

    private void Start()
    {
        m_BodyPartsManager.characterBody.characterBodyParts[2].bodyPart = m_EquippableTorso.BodyPart;
        m_BodyPartsManager.characterBody.characterBodyParts[3].bodyPart = m_EquippablePants.BodyPart;
        m_BodyPartsManager.UpdateBodyParts();
    }

    public void SetEquippable(EquipabbleItemScriptable i_EquippableItem, List<ItemParameterStruct> i_ItemState)
    {
        if(m_EquippableTorso != null && i_EquippableItem.ClothesType == EClothesType.Torso)
        {
            m_InventoryData.AddItem(m_EquippableTorso, 1, m_ItemCurrentState);
            this.m_EquippableTorso = i_EquippableItem;
            m_BodyPartsManager.characterBody.characterBodyParts[2].bodyPart = i_EquippableItem.BodyPart;

        }

        if(m_EquippablePants != null && i_EquippableItem.ClothesType == EClothesType.Pants)
        {
            m_InventoryData.AddItem(m_EquippablePants, 1, m_ItemCurrentState);
            this.m_EquippablePants = i_EquippableItem;
            m_BodyPartsManager.characterBody.characterBodyParts[3].bodyPart = i_EquippableItem.BodyPart;
        }

        m_BodyPartsManager.UpdateBodyParts();

        this.m_ItemCurrentState = new List<ItemParameterStruct>(i_ItemState);
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        foreach(var parameter in m_ParametersToModify)
        {
            if (m_ItemCurrentState.Contains(parameter))
            {
                int index = m_ItemCurrentState.IndexOf(parameter);
                float newValue = m_ItemCurrentState[index].Value + parameter.Value;

                m_ItemCurrentState[index] = new ItemParameterStruct
                {
                    ItemParameterScriptable = parameter.ItemParameterScriptable,
                    Value = newValue
                };
            }
        }
    }
}
