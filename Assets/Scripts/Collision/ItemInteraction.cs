using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : CollidableObject
{
    [SerializeField]
    private InventoryScriptable m_InventoryData;
    
    protected override void OnCollided(GameObject collidedObject)
    {
  
        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MoneyBag")
        {
            PickableItem Item = collision.GetComponent<PickableItem>();
            if (Item != null)
            {
                int reminder = m_InventoryData.AddItem(Item.InventoryItem, Item.Quantity);
                if (reminder == 0)
                    Item.DestroyItem();
                else
                    Item.Quantity = reminder;

            }
        }
        
    }

}
