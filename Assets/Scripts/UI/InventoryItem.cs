using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField]
    private Image m_ItemImage;

    [SerializeField]
    private TMP_Text m_QuantityText;

    [SerializeField]
    private Image m_BorderImage;

    private bool m_Empty = true;

    public event Action<InventoryItem> OnItemClicked,
        OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
        OnRightMouseButtonClick;


    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void Deselect()
    {
        m_BorderImage.enabled = false;
       
    }

    public void ResetData()
    {
        this.m_ItemImage.gameObject.SetActive(false);
        m_Empty = true;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.m_ItemImage.gameObject.SetActive(true);
        this.m_ItemImage.sprite = sprite;
        this.m_QuantityText.text = quantity.ToString();
        this.m_Empty = false;
    }

    public void Select()
    {
        m_BorderImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (m_Empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
