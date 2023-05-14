using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(GameManager.ShopController.ShopPage.isActiveAndEnabled == false)
            {
                GameManager.InventoryController.InventorySwitch();
            }
            
        }
    }
}
