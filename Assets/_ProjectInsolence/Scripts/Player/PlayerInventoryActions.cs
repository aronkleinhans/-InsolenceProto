using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.Core
{
    public class PlayerInventoryActions : MonoBehaviour
    {
        float pushForce = 5f;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Inventory inv = gameObject.GetComponent<Inventory>();

            if (Input.GetKeyDown(KeyCode.Q))
            {
               
            }
            if (Input.GetKeyDown(KeyCode.G)){
                inv.DropItem(inv.equippedInRightHandSlot);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                inv.CycleRightHandWeapons();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                inv.DualWieldWeapons();
            }
        }
    }
}
