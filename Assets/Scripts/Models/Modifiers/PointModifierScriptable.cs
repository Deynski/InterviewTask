using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PointModifierScriptable : StatModifierScriptable
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Point point = character.GetComponent<Point>();

        if(point != null )
        {
            point.AddPoints((int)val);
        }
    }
}
