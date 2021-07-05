using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiHandler : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction aimInput;

    public GameObject normalDecal;
    public GameObject aimingDecal;

    private void Awake()
    {
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
        normalDecal.SetActive(true);
        aimingDecal.SetActive(false);
    }

    private void StartAim()
    {
        normalDecal.SetActive(false);
        aimingDecal.SetActive(true);
    }
}
