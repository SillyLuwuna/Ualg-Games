using UnityEngine;

public class MoveCar : MonoBehaviour
{
	public float maxSpeed;
	public float turnSpeed;
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
			float accel = driver.accelerate(maxSpeed) * Time.deltaTime;
			float torque = driver.turn(turnSpeed) * Time.deltaTime;

			Vector3 currPos = transform.position;
			UpdateMovement(accel, torque);
			Vector3 updatedPos = transform.position;
			UpdateFuel(currPos, updatedPos);
		}
		else
		{
			fuel = 0;
		}
    }

	private void UpdateMovement(float accel, float torque)
	{
		Vector3 rotation = new Vector3(0, torque, 0);
		transform.Rotate(rotation);

		Vector3 translation = transform.forward * accel;
		transform.position += translation;
		// transform.Translate(translation);
	}

	private void UpdateFuel(Vector3 currPos, Vector3 updatedPos)
	{
		Vector3 diff = updatedPos - currPos;
		fuel -= diff.magnitude;
	}
}
