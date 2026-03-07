using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCamera : MonoBehaviour
{
	public Camera _firstPersonCamera;
	public Camera _thirdPersonCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		_firstPersonCamera.enabled = false;
		_thirdPersonCamera.enabled = true;
    }

	void OnSwitchPov()
	{
		SwapCameras();
	}

	private void SwapCameras()
	{
		_firstPersonCamera.enabled = !_firstPersonCamera.enabled;
		_thirdPersonCamera.enabled = !_thirdPersonCamera.enabled;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
