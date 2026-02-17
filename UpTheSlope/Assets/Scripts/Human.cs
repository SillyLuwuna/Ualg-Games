using UnityEngine;

public class Human : Driver
{
	public override Vector3 move(float maxSpeed)
	{
		float dx = Input.GetAxis("Horizontal");
		float dy = Input.GetAxis("Vertical");

		return new Vector3(dx, 0, dy) * maxSpeed;
	}
}
