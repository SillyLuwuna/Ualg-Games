using UnityEngine;

public interface IDriver
{
	float accelerate(float maxSpeed);
	float turn(float maxSpeed);
}
