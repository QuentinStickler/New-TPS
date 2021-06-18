using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class SwitchCam : MonoBehaviour
{
    CinemachineVirtualCamera cam;

    public PlayerInput playerInput;
    private InputAction aimInput;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        aimInput = playerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        aimInput.performed += _ => StartAim();
        aimInput.canceled += _ => StopAim();
    }

    private void OnDisable()
    {
        aimInput.performed -= _ => StartAim();
        aimInput.canceled -= _ => StopAim();
    }

    private void StopAim()
    {
        cam.Priority -= 10;
    }

    private void StartAim()
    {
        cam.Priority += 10;

    }
}
