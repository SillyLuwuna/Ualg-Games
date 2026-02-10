using UnityEngine;

public class BallSpawner : MonoBehaviour
{
	public GameObject ball;

	public float initialSpawn = 3.0f;
	public float spawnTimer = 5.0f;

	public float xmin = -3;
	public float xmax = 3;
	public float zmin = -50;
	public float zmax = 50;
	public float ymin = 10;
	public float ymax = 20;

    void Start()
    {
		InvokeRepeating("AddBalls", initialSpawn, spawnTimer);
    }

	void AddBalls()
	{
		float x = Random.Range(xmin, xmax);
		float y = Random.Range(ymin, ymax);
		float z = Random.Range(zmin, zmax);
		Vector3 pos = new Vector3(x, y, z);

		Instantiate(ball, pos, Quaternion.identity);
	}

    void Update()
    {
        
    }
}
