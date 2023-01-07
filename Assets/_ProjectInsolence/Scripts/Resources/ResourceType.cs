using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.Core
{
    [CreateAssetMenu(fileName = "New Farmable Resource", menuName = "Insolence/Resource")]
    public class ResourceType : ScriptableObject
    {
        [SerializeField] string name;
        [SerializeField] string type;
        [SerializeField] int initialAmount;
        [SerializeField] int amount;
        [SerializeField] Item item;
    }
}


