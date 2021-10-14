using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsResetter : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		iResetable reset = other.transform.GetComponentInParent(typeof(iResetable)) as iResetable;
		if(reset != null)
		{
			reset.Reset();
		}
	}
}
