using UnityEngine;

abstract public class Driver : MonoBehaviour, IDriver
{
	public abstract float accelerate(float maxSpeed);
	public abstract float turn(float maxSpeed);
}
