using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.Core
{
    public class DropItemTest : MonoBehaviour
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

            if (Input.GetKeyDown(KeyCode.G)){
                Item item = inv.equippedInHands;
                inv.DropItem(item); 
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                inv.SwapItem();
            }
        }
    }
}
