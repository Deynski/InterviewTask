using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas m_Canvas;

    

    [SerializeField]
    private InventoryItem m_InventoryItem;

    public void Awake()
    {
        m_Canvas = transform.root.GetComponent<Canvas>();
        m_InventoryItem.GetComponentInChildren<InventoryItem>();
    }

    public void SetData(Sprite i_Sprite, int i_Quantity)
    {
        m_InventoryItem.SetData(i_Sprite, i_Quantity);
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)m_Canvas.transform, 
            Input.mousePosition, 
            m_Canvas.worldCamera, out position);

        transform.position = m_Canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool i_Value)
    {
        gameObject.SetActive(i_Value);
    }
}
