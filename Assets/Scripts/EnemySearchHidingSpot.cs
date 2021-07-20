using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemySearchHidingSpot : MonoBehaviour
{
    private float searchRadius = 10f;
    private NavMeshAgent agent;
    private EnemyHealth health;
    private bool isGoingToHealthPickUp = false;
    private Vector3 walkPoint;
    private GameObject currentHealthPickUp;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        GoToWalkPoint();
    }

    private void LookforHidingSpot()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (var searchedColliders in colliders)
        {
            if (searchedColliders.gameObject.CompareTag("HealthPickUp"))
            {
                currentHealthPickUp = searchedColliders.gameObject;
                walkPoint = searchedColliders.gameObject.transform.position;
                isGoingToHealthPickUp = true;
            }
        }
    }

    private void GoToWalkPoint()
    {
        if (health.GetHealth() <= 50f && isGoingToHealthPickUp == false)
        {
            LookforHidingSpot();
        }
        else if (health.GetHealth() <= 50f && isGoingToHealthPickUp)
        {
            agent.SetDestination(walkPoint);
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            if (distanceToWalkPoint.magnitude < 1f)
            {
                health.RegenHealth(25f);
                Destroy(currentHealthPickUp);
                StartCoroutine(WaitOnWalkPoint());
            }
        }
    }
    IEnumerator WaitOnWalkPoint()
    {
        yield return new WaitForSeconds(2f);
        isGoingToHealthPickUp = false;
    }
}
