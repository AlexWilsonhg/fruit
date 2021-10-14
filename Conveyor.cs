using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
	[SerializeField] private Transform conveyorSectionPrefab;
	[SerializeField] private Transform conveyorSectionSpawnPointSeed;
	[SerializeField] public int maxConveyorSections = 8;
	[SerializeField] public int conveyorSpeed = 0;

	private List<Transform> conveyorSections = new List<Transform>();
    private bool active = false;
	


    // Start is called before the first frame update
    void Start()
    {
    	
    	for(int i = 0; i < maxConveyorSections; ++i)
    	{
			SpawnNewConveyorSection();
    	}
    
    }

    void FixedUpdate()
    {
        if(active)
        {
        	foreach(Transform section in conveyorSections)
        	{
        		section.Translate(-Vector3.forward * conveyorSpeed * Time.deltaTime);
        	}
        }
    }

    public void PauseConveyor()
    {
        active = false;
    }

    public void StartConveyor()
    {
        active = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
    	if(collision.tag == "ConveyorSection")
    	{
    		conveyorSections.Remove(collision.transform);
    		Destroy(collision.gameObject);
    		SpawnNewConveyorSection();
    	}
    }

   private void SpawnNewConveyorSection()
    {
    	if(conveyorSections.Count == 0)
    	{
    		conveyorSections.Add(Instantiate(conveyorSectionPrefab, conveyorSectionSpawnPointSeed));

    	}
    	else
    	{
    		Transform conveyor = conveyorSections[conveyorSections.Count - 1];
    		Transform spawnPoint = conveyor.Find("Spawn Offset");
    		conveyorSections.Add(Instantiate(conveyorSectionPrefab, spawnPoint.position, spawnPoint.rotation, conveyorSectionSpawnPointSeed));
    	}
    }
}
