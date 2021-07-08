using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Current enemy health: " + health);
        if (health <= 0) { Die(); }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}