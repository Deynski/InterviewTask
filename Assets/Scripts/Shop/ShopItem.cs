using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Image m_ItemImage;

    [SerializeField]
    private TMP_Text m_ItemName;

    [SerializeField]
    private TMP_Text m_Description;

    [SerializeField]
    private TMP_Text m_Price;

    [SerializeField]
    private ItemScriptable m_Item;

    [SerializeField]
    private List<ItemParameterStruct> m_Parameters;

    public event Action<ShopItem> OnButtonClick;
       

    public void SetData(Sprite i_Sprite, string i_ItemName, string i_Description, int i_Price, ItemScriptable i_Item)
    {
        this.m_ItemImage.gameObject.SetActive(true);
        this.m_ItemName.text = i_ItemName;
        this.m_ItemImage.sprite = i_Sprite;
        this.m_Description.text = i_Description;
        this.m_Price.text = "Buy" + "(" + i_Price.ToString() + " Coins"  +  ")";
        this.m_Item = i_Item;
    }

    public void OnBuyItem()
    {
        OnButtonClick?.Invoke(this);
    }

    public ItemScriptable BoughtItem { get { return m_Item; } }
    public List<ItemParameterStruct> Parameter { get { return m_Parameters; } }
}
