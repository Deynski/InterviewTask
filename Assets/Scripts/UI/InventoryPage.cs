using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem m_ItemPrefab;

    [SerializeField]
    private InventoryDescription m_ItemDescription;

    [SerializeField] 
    private MouseFollower m_MouseFollower;

    [SerializeField]
    private RectTransform m_ContentPanel;

    List<InventoryItem> listOfItems = new List<InventoryItem>();

    private int m_DraggedItemIndex = - 1;

    public event Action<int> OnDescriptionRequested,OnItemActionRequested,OnStartDragging;
    public event Action<int, int> OnSwapItems;


    private void Awake()
    {
        Hide();
        m_MouseFollower.Toggle(false);
        m_ItemDescription.ResetDescription();
    }
    public void InitializeInventoryUI(int i_InventorySize)
    {
        for (int i = 0; i < i_InventorySize; i++) 
        {
            InventoryItem item = Instantiate(m_ItemPrefab, Vector3.zero,Quaternion.identity);
            item.transform.SetParent(m_ContentPanel);
            item.transform.localScale = Vector3.one;
            listOfItems.Add(item);

            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnRightMouseButtonClick += HandleShowItemActions;

        }
    }

    public void UpdateData(int i_ItemIndex, Sprite i_ItemImage, int i_ItemQuantity)
    {
        if(listOfItems.Count > i_ItemIndex)
        {
            listOfItems[i_ItemIndex].SetData(i_ItemImage, i_ItemQuantity);
        }
    }

    private void HandleShowItemActions(InventoryItem i_InventoryItem)
    {
        int index = listOfItems.IndexOf(i_InventoryItem);
        if (index == -1)
        {
            return;
        }
        OnItemActionRequested?.Invoke(index); 
    }

    private void HandleEndDrag(InventoryItem i_InventoryItem)
    {
        ResetDraggedItem();
        m_MouseFollower.Toggle(false);
    }

    private void HandleSwap(InventoryItem i_InventoryItem)
    {
        int index = listOfItems.IndexOf(i_InventoryItem);
        if (index == -1)
        {
            return;
        }

        OnSwapItems?.Invoke(m_DraggedItemIndex, index);
        HandleItemSelection(i_InventoryItem);
    }

    private void ResetDraggedItem()
    {
        m_MouseFollower.Toggle(false);
        m_DraggedItemIndex = -1;
    }

    private void HandleBeginDrag(InventoryItem i_InventoryItem)
    {
        int index = listOfItems.IndexOf(i_InventoryItem);
        if (index == -1)
            return;
        m_DraggedItemIndex = index;
        HandleItemSelection(i_InventoryItem);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite i_Sprite, int i_ItemQuantity)
    {
        m_MouseFollower.Toggle(true);
        m_MouseFollower.SetData(i_Sprite, i_ItemQuantity);
    }

    private void HandleItemSelection(InventoryItem i_InventoryItem)
    {
        int index = listOfItems.IndexOf(i_InventoryItem);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        m_ItemDescription.ResetDescription();
        ResetSelection();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    private void ResetSelection()
    {
        m_ItemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (InventoryItem item in listOfItems)
        {
            item.Deselect();
        }
    }

    internal void UpdateDescription(int i_ItemIndex, Sprite i_ItemImage, string i_Name, string i_Description)
    {
        m_ItemDescription.SetDescription(i_ItemImage, i_Name, i_Description);
        DeselectAllItems();
        listOfItems[i_ItemIndex].Select();
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
