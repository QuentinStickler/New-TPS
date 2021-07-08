using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UiHandler : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction aimInput;

    public GameObject normalDecal;
    public GameObject aimingDecal;
    public TextMeshProUGUI currentBulletsText;
    public TextMeshProUGUI magazineSizeText;

    private PlayerShoot playerShoot;

    private void Awake()
    {
        aimInput = playerInput.actions["Aim"];
        playerShoot = GameObject.Find("Player").GetComponent<PlayerShoot>();
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

    private void Update()
    {
        currentBulletsText.text = playerShoot.GetCurrentBullets().ToString();
        magazineSizeText.text = playerShoot.GetMagazineSize().ToString();
    }
}
