using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrabber : MonoBehaviour
{
	[SerializeField] private float heldObjectDistance = 2.0f;
	[SerializeField] private Vector3 heldObjectOffset;
	[SerializeField] private float reachDistance = 5.0f;
	[SerializeField] private float throwStrength = 5.0f;
	[SerializeField] private Transform playerHead;

	private Transform grabbedObject;
	private Transform lookedAtObject;
	private float timeSinceLastAction = 0.0f;
	private float timeSinceLastActionToHint = 5.0f;
	private Text actionHint;

	void Awake()
	{
		actionHint = GameObject.Find("UI/HUD/Input Hint").GetComponent(typeof(Text)) as Text;
	}

	void Update()
	{
		CheckForGrabbable();
		UpdateInputHintTimer();

		if(Input.GetKeyDown("f"))
		{
			if(!grabbedObject)
			{
				GrabObject();
			}
			else
			{
				ThrowObject();
			}
		}
		if(Input.GetKeyDown("e"))
		{
			if(grabbedObject)
			{
				DropObject();
			}
		}
	}

	void LateUpdate()
	{
		if(grabbedObject)
		{
			UpdateHeldItemPos();
		}
	}

    public void GrabObject()
    {
    	if(lookedAtObject != null)
    	{
    		ResetLastActionTimer();
    		SetThrowDropHint();
    		grabbedObject = lookedAtObject;
	    	Rigidbody rb = grabbedObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
	    	rb.isKinematic = true;
	    }
    }

    public void ThrowObject()
    {
        if(grabbedObject != null)
        {
        	ResetLastActionTimer();
        	SetGrabHint();
        	Rigidbody rb = grabbedObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
        	rb.isKinematic = false;
        	rb.AddForce(playerHead.forward * throwStrength, ForceMode.Impulse);
        	grabbedObject = null;
        }
    }

    public void DropObject()
    {
        if(grabbedObject != null)
        {
        	ResetLastActionTimer();
        	SetGrabHint();
        	Rigidbody rb = grabbedObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
        	rb.isKinematic = false;
        	grabbedObject = null;
        }
    }

    private void CheckForGrabbable()
	{
		RaycastHit hit;
    	if(Physics.Raycast(playerHead.position, playerHead.forward, out hit, reachDistance))
    	{
    		if(hit.transform.GetComponent(typeof(Grabbable)) != null)
    		{
    			lookedAtObject = hit.transform;
    		}
    		else
    		{
    			lookedAtObject = null;
    		}
    	}
    	else
    	{
    		lookedAtObject = null;
    	}
	}

    private void UpdateHeldItemPos()
    {
    	grabbedObject.position = Vector3.MoveTowards(grabbedObject.position, playerHead.position + playerHead.forward + heldObjectOffset, 10.0f);
    	grabbedObject.rotation = playerHead.rotation;
    	grabbedObject.Rotate(0.0f, 90.0f, -45.0f);
    }

    private void UpdateInputHintTimer()
    {
    	timeSinceLastAction += Time.deltaTime;
    	if(timeSinceLastAction >= timeSinceLastActionToHint)
    	{
    		if(grabbedObject || lookedAtObject)
    		{
				ShowHint();
    		}
    		else
    		{
    			HideHint();
    		}
    	}
    }

    private void SetGrabHint()
    {
    	actionHint.text = "Press (F) to grab.";
    }

    private void SetThrowDropHint()
    {
    	actionHint.text = "Press (F) to throw or (E) to drop.";
    }

    private void ShowHint()
    {
    	actionHint.enabled = true;
    }

    private void HideHint()
    {
    	actionHint.enabled = false;
    }

    private void ResetLastActionTimer()
    {
    	timeSinceLastAction = 0.0f;
    	HideHint();
    }
}