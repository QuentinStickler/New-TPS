using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private GameObject player;
    public List<Behaviour> componentsToDisable;
    public Transform respawnPoint;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void HandleDeath()
    {
        player.GetComponent<PlayerHealth>().SetisDead(true);
        foreach (var disableComponents in componentsToDisable)
        {
            disableComponents.enabled = false;
        }
        Invoke("Respawn",3f);
        
    }

    public void Respawn()
    {
        player.GetComponent<PlayerHealth>().SetisDead(false);
        player.transform.position = respawnPoint.transform.position;
        foreach (var disableComponents in componentsToDisable)
        {
            disableComponents.enabled = true;
        }
        player.GetComponent<Animator>().SetBool("isDead",false);
    }
}
