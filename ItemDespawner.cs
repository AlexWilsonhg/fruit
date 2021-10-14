using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDespawner : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
    	if(other.tag == "Fruit")
    	{
    		Destroy(other.gameObject);
    	}
    }
}
