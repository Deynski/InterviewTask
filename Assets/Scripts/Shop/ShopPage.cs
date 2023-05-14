using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopPage : MonoBehaviour
{
    [SerializeField]
    private ShopItem i_ShopItem;
    
    [SerializeField]
    private RectTransform m_ContentPanel;

    [SerializeField]
    private AudioClip m_Purchased,m_CannotPurchase;

    [SerializeField]
    private AudioSource m_Source;

    [SerializeField]
    private int m_ShopSize = 0;

    List<ShopItem> listOfItems = new List<ShopItem>();

    

    private void Awake()
    {
        InitializeShopUI(m_ShopSize);
        Hide();
    }

    public void InitializeShopUI(int i_ShopSize)
    {
        for (int i = 0; i < i_ShopSize; i++)
        {
            ShopItem item = Instantiate(i_ShopItem, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(m_ContentPanel);
            item.transform.localScale = Vector3.one;
            listOfItems.Add(item);

            item.OnButtonClick += HandleBuyItem;
        }
        
    }

    private void HandleBuyItem(ShopItem i_ShopItem)
    {
        int currentMoney = GameManager.Player.GetComponent<Point>().Money;
        if(currentMoney >= i_ShopItem.BoughtItem.Price)
        {
            GameManager.InventoryController.Inventory.AddItem(i_ShopItem.BoughtItem, 1, i_ShopItem.Parameter);
            GameManager.Player.GetComponent<Point>().ConsumePoints(i_ShopItem.BoughtItem.Price);
            m_Source.PlayOneShot(m_Purchased);


        }
        else
        {
            Debug.Log("Cannot Buy");
            m_Source.PlayOneShot(m_CannotPurchase);
        }
       
    }

    public void UpdateData(int i_ItemIndex, Sprite i_ItemImage, string i_ItemName, string i_Description, int i_Price, ItemScriptable i_Item)
    {
        listOfItems[i_ItemIndex].SetData(i_ItemImage, i_ItemName, i_Description, i_Price, i_Item);
    }

    public void Show()
    {
        gameObject.SetActive(true);
     
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void HideShop()
    {
        gameObject.SetActive(false);
        GameManager.Instance.PlayerStopMoving = false;
    }
}
