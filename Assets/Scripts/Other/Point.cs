using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int Points = 0;
    
    public void AddPoints (int i_Modifier)
    {
        Points += i_Modifier;
    }
}
