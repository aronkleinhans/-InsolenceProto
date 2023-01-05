using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resource")]
public class ResourceType : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string type;
    [SerializeField] int amount;
}
