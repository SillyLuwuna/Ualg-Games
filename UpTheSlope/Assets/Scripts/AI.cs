using UnityEngine;

public class AI : Driver
{
	public float DistanceForActivation = 10;

	private ObjectTrackerSensor _sensor;

	void Start()
	{
		_sensor = this.GetComponent<ObjectTrackerSensor>();
		_sensor.TrackedObject = GameReferences.Player;
	}


	bool IsPlayerNear()
	{
		return _sensor.Distance <= DistanceForActivation;
	}

	public override float accelerate(float maxSpeed)
	{
		return maxSpeed;
	}

	public override float turn(float maxSpeed)
	{
		if (IsPlayerNear())
		{
			Vector3 crossProd = -Vector3.Cross(this.transform.forward, _sensor.Direction); // UNITY IS LEFT HANDED!?!?!?!
			float dir = Vector3.Dot(crossProd, transform.up);
			if (dir >= 0)
			{
				// left
				return -maxSpeed;
			}
			else
			{
				// right
				return maxSpeed;
			}
		}

		return 0;
	}
}
