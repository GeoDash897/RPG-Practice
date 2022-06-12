using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScriptObject : ScriptableObject
{
    [Multiline]
    [SerializeField] private string _developerInfo;
}
