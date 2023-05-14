using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingAreaInterAction : CollidableObject
{
    private const string DEFAULT_MESSAGE = "Town's Pawn Shop";
    private const string UPDATED_MESSAGE = "Press I to Sell Items";
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SellingArea")
        {
            GameManager.TextManager.UpdateSellingSign(UPDATED_MESSAGE);
            GameManager.Instance.PlayerInSellArea = true;
            GameManager.InventoryController.InventoryPage.Hide();
        }
    }



    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SellingArea")
        {
            GameManager.TextManager.UpdateSellingSign(DEFAULT_MESSAGE);
            GameManager.Instance.PlayerInSellArea = false;
            //GameManager.InventoryController.InventoryPage.ItemActionPanel.RemoveButton("Sell");
            
        }
    }
}
