using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Current health: " + health);
        if(health<= 0f){Debug.Log("You have dieded");}
    }
}
