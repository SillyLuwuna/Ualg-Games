using UnityEngine;

abstract public class Driver : MonoBehaviour, IDriver
{
	public abstract Vector3 move(float maxSpeed);
}
