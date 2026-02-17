using UnityEngine;

public class MoveCar : MonoBehaviour
{
	public float maxSpeed;
	public float fuel;

	[SerializeField]
	private Driver driver;

    void Start()
    {
    }

    void Update()
    {
		if (fuel > 0)
		{
			Vector3 dPosition = driver.move(maxSpeed) * Time.deltaTime;
			transform.position += dPosition;
			fuel -= dPosition.magnitude;
		}
		else
		{
			fuel = 0;
		}
    }
}
