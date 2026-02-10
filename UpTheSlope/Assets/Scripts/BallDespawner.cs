using UnityEngine;
using System.Collections;

public class BallDespawner : MonoBehaviour
{
	public float despawnTimer = 5.0f;

	private bool isFirst = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		StartCoroutine(Despawn());
    }

	IEnumerator Despawn()
	{
		if (isFirst)
		{
			yield return new WaitForSeconds(despawnTimer);
			isFirst = false;
		}
		Debug.Log("Ball despawned!");
		Destroy(gameObject);
		yield return null;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
