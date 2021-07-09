using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    private float health = 150f;
    private float damage = 10f;
    private float bulletSpeed = 15f;
    private bool isAttacking = false;
    private GameObject player;
    public List<Transform> bulletSpawnPoints;

    public GameObject bullet;

    private void Start()
    {
        player = GameObject.Find("Player");
        Debug.Log("Turret has started");
    }

    private void Update()
    {
        if(!isAttacking)StartCoroutine(AttackPlayer());
    }

    IEnumerator AttackPlayer()
    {
        Debug.Log("Turret attacking");
        isAttacking = true;
        foreach (var turretSpots in bulletSpawnPoints)
        {
            GameObject bulletInstance = Instantiate(bullet, turretSpots.position,Quaternion.identity);
            Vector3 direction = player.transform.position - transform.position;
            bulletInstance.GetComponent<Rigidbody>().AddForce(direction.normalized * bulletSpeed, ForceMode.Impulse);
            Destroy(bulletInstance,3f);   
        }
        yield return new WaitForSeconds(3);
        isAttacking = false;
    }
}
