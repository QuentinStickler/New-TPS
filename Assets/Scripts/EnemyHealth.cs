using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;
    private PlayerShoot playerShoot;

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
        this.gameObject.SetActive(false);
        playerShoot.SetScore(10f);
        Invoke("Respawn",3f);
    }

    private void Respawn()
    {
        this.gameObject.SetActive(true);
        health = 100f;
    }
}