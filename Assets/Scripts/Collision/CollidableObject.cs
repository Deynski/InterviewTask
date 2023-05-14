using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    private Collider2D m_Collider;
    private List<Collider2D> m_CollidedObjects = new List<Collider2D>(1);
    [SerializeField]
    private ContactFilter2D m_Filter;
    protected virtual void Start()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        m_Collider.OverlapCollider(m_Filter, m_CollidedObjects);

        foreach (var item in m_CollidedObjects)
        {
            OnCollided(item.gameObject);
        }
    }
    protected virtual void OnCollided (GameObject collidedObject)
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        

    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {

    }




}
