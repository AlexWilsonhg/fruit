using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZone : MonoBehaviour
{
    private List<GameObject> containedCrates = new List<GameObject>();

    public List<GameObject> GetItemCrates()
    {
    	return containedCrates;
    }

    public void OnTriggerEnter(Collider other)
    {
    	if(other.tag == "Crate")
    	{
    		containedCrates.Add(other.gameObject.transform.parent.gameObject);
    	}
    }

    public void OnTriggerExit(Collider other)
    {
    	containedCrates.Remove(other.gameObject);
    }

    public bool VerifyObjective(Objective objective)
    {
    	List<GameObject> crates = new List<GameObject>(containedCrates);
        if(crates.Count == 0 ) { return false; }

        foreach(ObjectiveListItem obj in objective.objectives)
        {
            bool isSatisfied = false;

            for(int i = crates.Count - 1; i >= 0; --i)
            {
                if(isSatisfied) { break; } // make sure we dont continue checking and removing crates if the obj is satisfied.

                ItemCrate c = crates[i].transform.GetComponent(typeof(ItemCrate)) as ItemCrate;
                if(c.type == obj.item.type && c.GetItemCount() >= obj.itemCount)
                {
                    isSatisfied = true;
                    crates.RemoveAt(i);
                }
            }

            if(isSatisfied == false) { return false; }
        }

        return true;
    }
}
