using UnityEngine;
using UnityEngine.InputSystem;

public class Human : Driver
{
	private Vector2 _dir = Vector2.zero;

	public override float accelerate(float maxSpeed)
	{
		return _dir.y * maxSpeed;
	}

	public override float turn(float maxSpeed)
	{
		return _dir.x * maxSpeed;
	}

	void OnMove(InputValue value)
	{
		_dir = value.Get<Vector2>();
	}
}
