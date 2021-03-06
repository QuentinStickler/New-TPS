using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;
    private float startHealth;
    private PlayerShoot playerShoot;
    public bool isRespawnable;
    public Image healthBar;
    public Canvas healthBarCanvas;
    private Camera mainCamera;

    private void Start()
    {
        playerShoot = GameObject.Find("Player").GetComponent<PlayerShoot>();
        startHealth = health;
        mainCamera = Camera.main;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0) { Die(); }
    }

    private void Update()
    {
        healthBarCanvas.transform.LookAt(mainCamera.transform.position);
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
        healthBar.fillAmount = health / startHealth;
    }

    public void RegenHealth(float regenHealth)
    {
        health += regenHealth;
        if (health > startHealth)
        {
            health = startHealth;
        }
        Debug.Log("Regenerated health:" + health);
        healthBar.fillAmount = health / startHealth;
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetStartHealth()
    {
        return startHealth;
    }
}