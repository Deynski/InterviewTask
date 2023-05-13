using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    List<InventoryItem> listOfItems = new List<InventoryItem>();

    public void InitializeInventoryUI(int i_InventorySize)
    {
        for (int i = 0; i < i_InventorySize; i++) 
        {
            InventoryItem item = Instantiate(itemPrefab,Vector3.zero,Quaternion.identity);
            item.transform.SetParent(contentPanel);
            item.transform.localScale = Vector3.one;
            listOfItems.Add(item);
        }
    }

    public void Show() // Can be merged into one method
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
