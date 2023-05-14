using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : CollidableObject
{

    private const string DEFAULT_MESSAGE = "Town's Shop";
    private const string UPDATED_MESSAGE = "Press F to Interact";

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ShopKeeper")
        {
            GameManager.TextManager.UpdateShopSign(UPDATED_MESSAGE);
            if (Input.GetKey(KeyCode.F))
            {
                if (GameManager.InventoryController.InventoryPage.isActiveAndEnabled == false)
                {
                    OnOpenShop(collision.gameObject);
                }
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ShopKeeper")
            GameManager.TextManager.UpdateShopSign(DEFAULT_MESSAGE);
    }

    private void OnOpenShop(GameObject collidedObject)
    {
        VendorNPC npc = collidedObject.GetComponentInParent<VendorNPC>();
        if (npc != null)
        {
            npc.OpenShop();
            GameManager.Instance.PlayerStopMoving = true;
            if (GameManager.InventoryController.InventoryPage.isActiveAndEnabled == true) 
            {
                GameManager.InventoryController.InventoryPage.Hide();
            }
        }
    }


}
