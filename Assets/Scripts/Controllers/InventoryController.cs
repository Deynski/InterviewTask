using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryPage m_inventoryPage;

    [SerializeField]
    private InventoryScriptable m_InventoryData;

    [SerializeField]
    private AudioClip m_DropClip;

    [SerializeField]
    private AudioSource m_AudioSource;

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
            m_inventoryPage.ShowItemAction(i_ItemIndex);
            if(GameManager.Instance.PlayerInSellArea == false)
            {
                m_inventoryPage.AddAction(itemAction.ActionName, () => PerformAction(i_ItemIndex));
            }
            
        }

        IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;

        if (destroyableItem != null)
        {
            //Put Condition where in Selling Territory
            if(GameManager.Instance.PlayerInSellArea == true)
            {
                m_inventoryPage.AddAction("Sell", () => DropItem(i_ItemIndex, inventoryItem.Quantity));
                
            }
        }

    }

    private void DropItem(int i_ItemIndex, int quantity)
    {
        //Put Condition where
        InventoryItemStruct inventoryItem = m_InventoryData.GetItemAt(i_ItemIndex);
        ItemScriptable itemScriptable = inventoryItem.Item;
       
        m_InventoryData.RemoveItem(i_ItemIndex, quantity);
        m_inventoryPage.ResetSelection();
        GameManager.Player.GetComponent<Point>().AddPoints(itemScriptable.SellAmount);
        m_AudioSource.PlayOneShot(m_DropClip);

    }

    public void PerformAction(int i_ItemIndex)
    {
        InventoryItemStruct inventoryItem = m_InventoryData.GetItemAt(i_ItemIndex);
        if (inventoryItem.IsEmpty) return;

        IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;

        if (destroyableItem != null)
        {
            m_InventoryData.RemoveItem(i_ItemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.Item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(GameManager.Player, inventoryItem.ItemStates);
            m_AudioSource.PlayOneShot(itemAction.ActionSFX);

            if (m_InventoryData.GetItemAt(i_ItemIndex).IsEmpty)
                m_inventoryPage.ResetSelection();
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
        {
            return;
        }
            
        ItemScriptable itemScriptable = inventoryItem.Item;
        string description = PrepareDescription(inventoryItem);
        m_inventoryPage.UpdateDescription(i_ItemIndex, itemScriptable.ItemImage, itemScriptable.name, description);
    }

    private string PrepareDescription(InventoryItemStruct i_InventoryItem)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(i_InventoryItem.Item.Description);
        sb.AppendLine();

        for (int i = 0; i < i_InventoryItem.ItemStates.Count; i++)
        {
            sb.Append($"{i_InventoryItem.ItemStates[i].ItemParameterScriptable.ParameterName}" 
                + $" : {i_InventoryItem.ItemStates[i].Value} / {i_InventoryItem.Item.Parameters[i].Value}");
        }
        return sb.ToString();
    }

    public void InventorySwitch()
    {
        if (m_inventoryPage.isActiveAndEnabled == false)
        {

            if (GameManager.Instance.PlayerInSellArea)
                GameManager.Instance.PlayerStopMoving = true;
            else
                GameManager.Instance.PlayerStopMoving = false;

            m_inventoryPage.Show();
            foreach (var item in m_InventoryData.GetCurrentInventoryState())
            {
                m_inventoryPage.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
            }
            
        }
        else
        {
            m_inventoryPage.Hide();
            GameManager.Instance.PlayerStopMoving = false;
        }
    }

    public InventoryPage InventoryPage { get => m_inventoryPage; }
    public InventoryScriptable Inventory { get => m_InventoryData; }
}
