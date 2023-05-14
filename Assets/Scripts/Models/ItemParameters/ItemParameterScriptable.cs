using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemParameterScriptable : ScriptableObject
{
    [field: SerializeField]
    public string ParameterName { get; private set; }

}
