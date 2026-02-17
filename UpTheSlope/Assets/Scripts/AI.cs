using UnityEngine;

public class AI : Driver
{
	public override Vector3 move(float maxSpeed)
	{
		return transform.forward * maxSpeed;
	}
}
