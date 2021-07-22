using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEnemyAI : MonoBehaviour
{

    public GameObject hidingSpot;
    public GameObject healthPickUp;
    public Transform hidingSpotSpawn;
    public Transform healthPickUpSpawn;
    private float searchRadius = 8f;
    private bool hasFoundWoundedAlly;
    
    private void Start()
    {
        hasFoundWoundedAlly = false;
    }
    private void Update()
    {
        if (hasFoundWoundedAlly == false)
        {
            LookForWoundedAllies();
        }
    }

    private void LookForWoundedAllies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (var searchedColliders in colliders)
        {
            if (searchedColliders.gameObject.CompareTag("Enemy") && !searchedColliders.gameObject.Equals(this.gameObject))
            {
                if (searchedColliders.gameObject.GetComponent<EnemyHealth>().GetHealth() <=
                    searchedColliders.gameObject.GetComponent<EnemyHealth>().GetStartHealth()/2)
                {
                    Debug.Log(searchedColliders.gameObject.name);
                    hasFoundWoundedAlly = true;
                    Instantiate(hidingSpot, hidingSpotSpawn.position, Quaternion.identity);
                    Instantiate(healthPickUp, healthPickUpSpawn.position, Quaternion.identity);
                    StartCoroutine(RechargeHidingSpot());
                }
            }
        }
    }

    IEnumerator RechargeHidingSpot()
    {
        yield return new WaitForSeconds(5f);
        hasFoundWoundedAlly = false;
    }
}
