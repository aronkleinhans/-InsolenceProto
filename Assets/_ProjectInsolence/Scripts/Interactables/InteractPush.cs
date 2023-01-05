using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPush : Interactable
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float pushForce = 20f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void Interaction(Transform playerTf)
    {
        Debug.Log("pushed " + transform.gameObject.name);

        rb.AddForce(playerTf.forward * pushForce, ForceMode.VelocityChange);
    }
}
