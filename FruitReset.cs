using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitReset : iResetable
{
   
	public override void Reset()
	{
		Destroy(transform.gameObject);
	}
}
