using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : CollidableObject
{
    private bool m_Interacted = false;
    protected override void OnCollided(GameObject collidedObject)
    {
        if (Input.GetKey(KeyCode.F))
        {
            OnInteract();
        }
    }

    private void OnInteract()
    {

        if(!m_Interacted)
        {
            m_Interacted = true;
            Debug.Log("Interacted with: " + name);
        }
        
       
    }


}
