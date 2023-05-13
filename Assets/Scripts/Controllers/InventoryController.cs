using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryPage m_inventoryPage;

    [SerializeField]
    private InventoryScriptable m_InventoryData;

    public int inventorySize = 10; // Temporary

    public List<InventoryItemStruct> InitialItems = new List<InventoryItemStruct>();

    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareInventoryData()
    {
        m_InventoryData.Initialize();
        m_InventoryData.OnInventoryUpdated += UpdateInventoryUI;

        foreach (InventoryItemStruct item in InitialItems)
        {
            if (item.IsEmpty) continue;

            m_InventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItemStruct> i_InventoryState)
    {
        m_inventoryPage.ResetAllItems();
        foreach (var item in i_InventoryState)
        {
            m_inventoryPage.UpdateData(item.Key, item.Value.Item.ItemImage,
                item.Value.Quantity);
        }
    }

    private void PrepareUI()
    {
        m_inventoryPage.InitializeInventoryUI(m_InventoryData.Size);
        this.m_inventoryPage.OnDescriptionRequested += HandleDescriptionRequest;
        this.m_inventoryPage.OnSwapItems += HandleSwapItems;
        this.m_inventoryPage.OnStartDragging += HandleDragging;
        this.m_inventoryPage.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int i_ItemIndex)
    {
        InventoryItemStruct inventoryItem = m_InventoryData.GetItemAt(i_ItemIndex);
        if(inventoryItem.IsEmpty) return;

        IItemAction itemAction = inventoryItem.Item as IItemAction;
        if(itemAction != null)
        {
            itemAction.PerformAction(gameObject);
        }
        IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;

        if(destroyableItem != null)
        {
            m_InventoryData.RemoveItem(i_ItemIndex, 1);
        }
    }

    private void HandleDragging(int i_ItemIndex)
    {
        InventoryItemStruct inventoryItem = m_InventoryData.GetItemAt(i_ItemIndex);

        if (inventoryItem.IsEmpty) return;

        m_inventoryPage.CreateDraggedItem(inventoryItem.Item.ItemImage, inventoryItem.Quantity);
    }

    private void HandleSwapItems(int i_ItemIndex1, int i_ItemIndex2)
    {
        m_InventoryData.SwapItems(i_ItemIndex1, i_ItemIndex2); 
    }

    private void HandleDescriptionRequest(int i_ItemIndex)
    {
        InventoryItemStruct inventoryItem = m_InventoryData.GetItemAt(i_ItemIndex);
        if (inventoryItem.IsEmpty)
            return;
        ItemScriptable itemScriptable = inventoryItem.Item;
        m_inventoryPage.UpdateDescription(i_ItemIndex, itemScriptable.ItemImage, itemScriptable.name, itemScriptable.Description);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(m_inventoryPage.isActiveAndEnabled == false)
            {
                m_inventoryPage.Show();
                foreach (var item in m_InventoryData.GetCurrentInventoryState())
                {
                    m_inventoryPage.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
                }
            }
            else
            {
                m_inventoryPage.Hide();
            }
        }
    }
}
