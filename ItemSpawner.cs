using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	[SerializeField] private int spawnSpeed = 1;
	[SerializeField] private List<Transform> items = new List<Transform>();
	[SerializeField] private float minSpawnPos = -0.5f;
	[SerializeField] private float maxSpawnPos = 0.5f;
	private float timeSinceLastSpawn = 0.0f;
    private bool active = true;

    void Update()
    {
        if(active)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if(timeSinceLastSpawn >= (5.0f/spawnSpeed))
            {
            	SpawnItem();
            	timeSinceLastSpawn = 0.0f;
            }
        }
    }

    private void SpawnItem()
    {
    	Transform item = items[Random.Range(0, items.Count)];
    	Vector3 spawnPoint = new Vector3(Random.Range(minSpawnPos, maxSpawnPos) + transform.position.x, transform.position.y, transform.position.z);
    	Instantiate(item, spawnPoint, transform.rotation, this.transform);
    }

    public void Play()
    {
        active = true;
    }

    public void Pause()
    {
        active = false;
    }
}
