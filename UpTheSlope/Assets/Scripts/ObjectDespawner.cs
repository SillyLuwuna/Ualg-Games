using UnityEngine;

public class ObjectDespawner : MonoBehaviour
{
	public float yTrigger = -100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (transform.position.y <= yTrigger)
		{
			Destroy(gameObject);
		}
    }
}
