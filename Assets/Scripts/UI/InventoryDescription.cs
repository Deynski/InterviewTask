using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image m_Itemimage;
    [SerializeField]
    private TMP_Text m_Title;
    [SerializeField]
    private TMP_Text m_Description;

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.m_Itemimage.gameObject.SetActive(false);
        this.m_Title.text = "";
        this.m_Description.text = "";
    }

    public void SetDescription(Sprite i_Sprite, string i_ItemName,string i_ItemDescription)
    {
        this.m_Itemimage.gameObject.SetActive(true);
        this.m_Itemimage.sprite = i_Sprite;
        this.m_Title.text = i_ItemName;
        this.m_Description.text = i_ItemDescription;
    }
}
