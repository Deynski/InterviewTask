using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryPage m_inventoryPage;

    public int inventorySize = 10; // Temporary


    private void Start()
    {
        m_inventoryPage.InitializeInventoryUI(inventorySize);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(m_inventoryPage.isActiveAndEnabled == false)
            {
                m_inventoryPage.Show();
            }
            else
            {
                m_inventoryPage.Hide();
            }
        }
    }
}
