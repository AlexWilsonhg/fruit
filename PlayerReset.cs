using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : iResetable
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
    	PlayerGrabber grabber = gameObject.GetComponent(typeof(PlayerGrabber)) as PlayerGrabber;
    	grabber.DropObject();

    	transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

}
