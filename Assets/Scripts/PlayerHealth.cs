using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100f;
    public Transform respawnPoint;
    private Animator animator;
    private bool isDead = false;
    public Die deathHandler;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetisDead(bool _isDead)
    {
        isDead = _isDead;
    }

    public bool GetisDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= damage;
            Debug.Log("Current health: " + health);
            if (health <= 0f)
            {
                animator.SetBool(("isDead"),true);
                deathHandler.HandleDeath();
            }
        }
    }
}
