using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_CoinText;

    [SerializeField]
    private TMP_Text m_ShopSignText;

    [SerializeField]
    private TMP_Text m_SellingSignText;

    public void UpdateCoinText(int i_Value)
    {
        m_CoinText.text = i_Value.ToString();
    }

    public void UpdateShopSign(string i_Value) 
    {
        m_ShopSignText.text = i_Value;
    }

    public void UpdateSellingSign(string i_Value)
    {
        m_SellingSignText.text = i_Value;
    }
}
