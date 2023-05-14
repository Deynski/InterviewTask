using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int Money = 0;
    private void Start()
    {
        GameManager.TextManager.UpdateCoinText(Money);
    }
    public void AddPoints (int i_Modifier)
    {
        Money += i_Modifier;
        GameManager.TextManager.UpdateCoinText(Money);
    }

    public void ConsumePoints(int i_Modifier)
    {
        if (Money < 0)
        {
            Money = 0;
        }
        else
        {
            Money -= i_Modifier;
        }
        GameManager.TextManager.UpdateCoinText(Money);
    }   
}
