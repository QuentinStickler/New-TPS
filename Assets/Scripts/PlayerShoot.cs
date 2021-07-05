using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    PlayerInput playerInput;
    public InputAction shootAction;
    public InputAction shootStopAction;
    public Transform shootingPoint;
    Camera camera;

    private float rateOfFire = 0.3f;
    public bool isAutomatic;

    public float bulletSpeed;
    [SerializeField]
    private bool allowInvoke;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
        shootAction.performed += x => Shoot();
        shootStopAction = playerInput.actions["ShootStop"];
        shootStopAction.performed += x => ShootStop();
        camera = Camera.main;
    }

    public void Shoot()
    {
        allowInvoke = true;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else targetPoint = ray.GetPoint(100);

        Vector3 distanceToPoint = targetPoint - shootingPoint.position;

        GameObject bulletInstance = Instantiate(bullet, shootingPoint.position, Quaternion.Euler(90f, 0f, 0f));

        bulletInstance.GetComponent<Rigidbody>().AddForce(distanceToPoint.normalized * bulletSpeed,ForceMode.Impulse);

        Destroy(bulletInstance, 3f);

    }

    private void Update()
    {
        if (isAutomatic && allowInvoke) { Invoke(nameof(Shoot), rateOfFire); }
    }
    private void ShootStop() { allowInvoke = false; }

}
