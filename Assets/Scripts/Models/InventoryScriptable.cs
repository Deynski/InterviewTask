using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class InventoryScriptable : ScriptableObject
{
    [SerializeField]
    private List<InventoryItemStruct> m_InventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    public event Action<Dictionary<int,InventoryItemStruct>> OnInventoryUpdated;

    public void Initialize()
    {
        m_InventoryItems = new List<InventoryItemStruct>();

        for (int i = 0; i < Size; i++)
        {
            m_InventoryItems.Add(InventoryItemStruct.GetEmptyItem());
        }
    }

    public int AddItem(ItemScriptable i_Item, int i_Quantity)
    {
        if (i_Item.IsStackable == false)
        {
            for (int i = 0; i < m_InventoryItems.Count; i++)
            {
             
                while (i_Quantity > 0 && IsInventoryFull() == false)
                {
                    i_Quantity -= AddItemToFirstFreeSlot(i_Item, 1);
                    
                }
                InformAboutChange();
                return i_Quantity;
                
            }
        }
        i_Quantity = AddStackableItem(i_Item, i_Quantity);
        InformAboutChange();
        return i_Quantity;
    }

    private int AddItemToFirstFreeSlot(ItemScriptable i_Item, int i_Quantity)
    {
        InventoryItemStruct newItem = new InventoryItemStruct
        {
            Item = i_Item,
            Quantity = i_Quantity
        };

        for (int i = 0; i < m_InventoryItems.Count; i++)
        {
            if (m_InventoryItems[i].IsEmpty)
            {
                m_InventoryItems[i] = newItem;
                return i_Quantity;
            }
        }
        return 0;

    }

    private bool IsInventoryFull() => m_InventoryItems.Where(item => item.IsEmpty).Any() == false;
    

    private int AddStackableItem(ItemScriptable i_Item, int i_Quantity)
    {
        for (int i = 0; i < m_InventoryItems.Count; i++)
        {
            if (m_InventoryItems[i].IsEmpty) continue;

            if (m_InventoryItems[i].Item.ID  == i_Item.ID)
            {
                int amountToTake = m_InventoryItems[i].Item.MaxStackSize - m_InventoryItems[i].Quantity;

                if (i_Quantity > amountToTake)
                {
                    m_InventoryItems[i] = m_InventoryItems[i].ChangeQuantity(m_InventoryItems[i].Item.MaxStackSize);
                    i_Quantity -= amountToTake;
                }
                else
                {
                    m_InventoryItems[i] = m_InventoryItems[i].ChangeQuantity(m_InventoryItems[i].Quantity + i_Quantity);
                    InformAboutChange();
                    return 0;
                }
            }

        }

        while (i_Quantity > 0 && IsInventoryFull() == false )
        {
            int newQuantity = Mathf.Clamp(i_Quantity, 0, i_Item.MaxStackSize);
            i_Quantity -= newQuantity;
            AddItemToFirstFreeSlot(i_Item, newQuantity);
        }

        return i_Quantity;
       
    }

    public Dictionary<int, InventoryItemStruct> GetCurrentInventoryState()
    {
        Dictionary<int,InventoryItemStruct> returnValue = new Dictionary<int, InventoryItemStruct>();

        for(int i = 0;i < m_InventoryItems.Count;i++)
        {
            if (m_InventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = m_InventoryItems[i];
        }
        return returnValue;
    }

    public InventoryItemStruct GetItemAt(int i_ItemIndex)
    {
        return m_InventoryItems[i_ItemIndex];
    }

    public void AddItem(InventoryItemStruct i_Item)
    {
        AddItem(i_Item.Item,i_Item.Quantity);
    }

    public void SwapItems(int i_ItemIndex1, int i_ItemIndex2)
    {
        InventoryItemStruct item1 = m_InventoryItems[i_ItemIndex1];

        m_InventoryItems[i_ItemIndex1] = m_InventoryItems[i_ItemIndex2];
        m_InventoryItems[i_ItemIndex2] = item1;

        InformAboutChange();
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }

    public void RemoveItem(int i_ItemIndex, int i_Amount)
    {
        if (m_InventoryItems.Count > i_ItemIndex)
        {
            if (m_InventoryItems[i_ItemIndex].IsEmpty) return;

            int reminder = m_InventoryItems[i_ItemIndex].Quantity - i_Amount;
            if (reminder <= 0)
                m_InventoryItems[i_ItemIndex] = InventoryItemStruct.GetEmptyItem();
            else 
                m_InventoryItems[i_ItemIndex] = m_InventoryItems[i_ItemIndex].ChangeQuantity(reminder);

            InformAboutChange();
                
            
        }
    }
}
[System.Serializable]
public struct InventoryItemStruct
{
    public int Quantity;
    public ItemScriptable Item;

    public bool IsEmpty => Item == null;

    public InventoryItemStruct ChangeQuantity(int quantity)
    {
        return new InventoryItemStruct
        {
            Item = this.Item,
            Quantity = quantity
        };
    }

    public static InventoryItemStruct GetEmptyItem() => new InventoryItemStruct
    {
        Item = null,
        Quantity = 0
    };
}
