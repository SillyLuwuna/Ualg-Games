using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCamera : MonoBehaviour
{
	public Camera firstPersonCamera;
	public Camera thirdPersonCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		firstPersonCamera.enabled = false;
		thirdPersonCamera.enabled = true;
    }

	void OnSwitchPov()
	{
		SwapCameras();
	}

	private void SwapCameras()
	{
		firstPersonCamera.enabled = !firstPersonCamera.enabled;
		thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
