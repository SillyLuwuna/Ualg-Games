using System.Collections;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
	public float spawnTimer;
	public GameObject[] vehicles;
	public Vector3 spawnPosition;
	public Quaternion spawnRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		StartCoroutine(SpawnCar());
    }

	IEnumerator SpawnCar()
	{
		while (true)
		{
			int rngIndex = (int) Mathf.Round(Random.Range(0, vehicles.Length));
			GameObject vehicle = Instantiate(vehicles[rngIndex], spawnPosition, spawnRotation);

			yield return new WaitForSeconds(spawnTimer);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
