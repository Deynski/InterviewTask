using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorNPC : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;

    public void OpenShop()
    {
        GameManager.ShopController.ShopPage.Show();
    }

    public void CloseShop()
    {
        GameManager.ShopController.ShopPage.Hide();
    }
}
