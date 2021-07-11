using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    private float speed = 3f;
    private NavMeshAgent navmeshAgent;
    [SerializeField]
    private bool walkPointSet = false;

    [SerializeField]
    private int currentPoint = 0;

    private Vector3 walkPoint;

    private void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        GoToPatrolPoint();
    }

    private void GoToPatrolPoint()
    {
        if(!walkPointSet){ SearchWalkPoint();}
        else navmeshAgent.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        Debug.Log(walkPoint);
        if (distanceToWalkPoint.magnitude < 1f)
        {
            Debug.Log("Walkpoint reached");
            walkPointSet = false;
            currentPoint++;
            Debug.Log("Size of List: "  + patrolPoints.Count + " CurrentIndex: " + currentPoint);
            if (currentPoint >= patrolPoints.Count) {currentPoint = 0;}
        }
    }
    private void SearchWalkPoint()
    {
        walkPoint = new Vector3(patrolPoints[currentPoint].transform.position.x, transform.position.y, patrolPoints[currentPoint].transform.position.z);
        walkPointSet = true;
    }
}
