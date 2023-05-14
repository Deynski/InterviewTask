using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemActionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ButtonPrefab;

    public void AddButton(string name, Action i_OnClickAction)
    {
        GameObject button = Instantiate(m_ButtonPrefab,transform);
        button.GetComponent<Button>().onClick.AddListener(() => i_OnClickAction());
        button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        button.name = name;
    }

    public void RemoveButton(string i_Name)
    {
        foreach (Transform child in transform)
        {
            if(child.name == i_Name)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void Toggle(bool val) 
    {
        if (val == true) RemoveOldButtons();
        gameObject.SetActive(val);
    }

    public void RemoveOldButtons()
    {
        foreach (Transform child  in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
