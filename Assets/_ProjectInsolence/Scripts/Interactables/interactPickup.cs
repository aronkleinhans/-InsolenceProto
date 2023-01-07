using Insolence.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactPickup : Interactable
{
    public override void Interaction(Transform tf)
    {
        ItemSOHolder item = GetComponent<ItemSOHolder>();
        Debug.Log(tf.gameObject.name + " attempts to pick up " + item.name);
        if (tf.GetComponent<Inventory>().AddItem(item.item))
        {
            Destroy(gameObject);
        }
    }
}
