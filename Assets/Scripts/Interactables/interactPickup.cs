using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactPickup : Interactable
{
    public override void Interaction(Transform tf)
    {
        Destroy(this.gameObject);
    }
}
