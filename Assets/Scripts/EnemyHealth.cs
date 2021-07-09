using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;
    private PlayerShoot playerShoot;
    public bool isRespawnable;

    private void Start()
    {
        playerShoot = GameObject.Find("Player").GetComponent<PlayerShoot>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) { Die(); }
    }

    private void Die()
    {
        playerShoot.SetScore(10f);
        if (isRespawnable)
        {
            this.gameObject.SetActive(false);
            Invoke("Respawn", 3f);
        }
        else Destroy(this.gameObject);
    }

    private void Respawn()
    {
        this.gameObject.SetActive(true);
        health = 100f;
    }
}