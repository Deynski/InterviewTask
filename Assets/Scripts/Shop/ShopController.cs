using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private ShopPage m_ShopPage;

    [SerializeField]
    private List<ItemScriptable> m_ItemsToSell;

    private void Start()
    {
        PrepareUI();
    }

    private void PrepareUI()
    {
        Debug.Log(m_ItemsToSell.Count);
        for (int i = 0; i < m_ItemsToSell.Count; i++)
        {
            m_ShopPage.UpdateData(i, m_ItemsToSell[i].ItemImage, m_ItemsToSell[i].Name, m_ItemsToSell[i].Description, m_ItemsToSell[i].Price, m_ItemsToSell[i]);
        }
    }

    public ShopPage ShopPage { get => m_ShopPage;}
    public List<ItemScriptable> ItemShopList { get => m_ItemsToSell; }
}
