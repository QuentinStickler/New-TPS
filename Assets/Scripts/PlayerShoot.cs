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
    public InputAction reloadAction;
    public Transform shootingPoint;
    Camera camera;

    private float rateOfFire = 0.1f;
    private int magazineSize = 30;
    private float reloadTime = 1.5f;
    public bool isReloading;
    private int currentBulletAmount;
    public bool isAutomatic;


    public float bulletSpeed;
    [SerializeField]
    private bool isAttackHeld;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
        reloadAction = playerInput.actions["Reload"];
        camera = Camera.main;
        currentBulletAmount = magazineSize;
    }
    //Das Shootzeug auslagern und in ne seperate Klasse stecken und von vornerein sagen, ob es automatic sein soll oder nich

    void OnEnable()
    {
        shootAction.performed += x => HandleShoot();
        shootAction.canceled += x => HandleStopShooting();
        reloadAction.performed += x => StartCoroutine(Reload());
    }

    void OnDisable()
    {
        shootAction.performed -= x => HandleShoot();
        shootAction.canceled -= x => HandleStopShooting();
        reloadAction.performed -= x => StartCoroutine(Reload());
    }

    IEnumerator AttackHoldCo()
    {
        while (isAttackHeld)
        {
            Shoot();
            yield return new WaitForSeconds(rateOfFire);
        }
    }

    public int GetCurrentBullets()
    {
        return currentBulletAmount;
    }

    public int GetMagazineSize()
    {
        return magazineSize;
    }

    private void Update()
    {
        if (currentBulletAmount <= 0 && !isReloading) { isReloading = true; StartCoroutine(Reload()); }
    }
    public void Shoot()
    {
        if (!isReloading)
        {
            currentBulletAmount--;
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

            bulletInstance.GetComponent<Rigidbody>().AddForce(distanceToPoint.normalized * bulletSpeed, ForceMode.Impulse);

            Destroy(bulletInstance, 3f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        currentBulletAmount = magazineSize;
    }
    private void HandleShoot()
    {
        isAttackHeld = true;
        StartCoroutine(AttackHoldCo());
    }
    private void HandleStopShooting()
    {
        if (isAttackHeld)
        {
            isAttackHeld = false;
        }
    }
}
