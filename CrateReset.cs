using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateReset : iResetable
{
	private Vector3 initialPosition;
	private Quaternion initialRotation;

    void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public override void Reset()
    {
    	transform.position = initialPosition;
    	transform.rotation = initialRotation;
    	ItemCrate crate = transform.gameObject.GetComponent(typeof(ItemCrate)) as ItemCrate;
    	crate.ResetCrateType();

    	Rigidbody rb = transform.gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
    	rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
