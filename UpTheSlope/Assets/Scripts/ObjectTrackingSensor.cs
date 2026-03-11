using System.Collections;
using UnityEngine;

public class ObjectTrackerSensor : MonoBehaviour
{
	public GameObject TrackedObject { get; set; }
	public float TrackingFrequency = 0.1f;

	private Vector3 _direction;

	public float Distance
	{
		get
		{
			if (_direction == null)
			{
				return float.PositiveInfinity;
			}

			return _direction.magnitude;
		}
	}

	public Vector3 Direction
	{
		get
		{
			if (_direction == null)
			{
				return Vector3.zero;
			}

			return _direction;
		}

		private set
		{
			_direction = value;
		}
	}

	public bool HasObject
	{
		get { return _direction != null; }
	}

    void Start()
    {
		StartCoroutine(TrackObject());
    }

	IEnumerator TrackObject()
	{
		while (true)
		{
			_direction = TrackedObject.transform.position - this.transform.position;
			yield return new WaitForSeconds(TrackingFrequency);
		}
	}
}
