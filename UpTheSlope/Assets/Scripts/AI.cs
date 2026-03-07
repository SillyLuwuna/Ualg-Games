using UnityEngine;

public class AI : Driver
{
	public override float accelerate(float maxSpeed)
	{
		// return transform.forward * maxSpeed;
		return maxSpeed;
	}

	public override float turn(float maxSpeed)
	{
		return 0;
	}
}
