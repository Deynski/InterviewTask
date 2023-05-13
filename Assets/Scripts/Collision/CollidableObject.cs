using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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
        Debug.Log("Collided With" + collidedObject.name);
    }


}
