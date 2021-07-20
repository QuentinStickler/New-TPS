using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public bool shouldWaitOnWalkPoints;
    private float speed = 3f;
    private NavMeshAgent navmeshAgent;
    [SerializeField]
    private bool walkPointSet = false;

    [SerializeField]
    private int currentPoint = 0;

    private Vector3 walkPoint;

    private float waitTime = 0;

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
        if (distanceToWalkPoint.magnitude < 1f)
        {
            currentPoint++;
            if (currentPoint >= patrolPoints.Count) {currentPoint = 0;}

            if (shouldWaitOnWalkPoints)
            {
                StartCoroutine("WaitOnWalkPoint");
            }
            else walkPointSet = false;
        }
    }

    IEnumerator WaitOnWalkPoint()
    {
        yield return new WaitForSeconds(1f);
        walkPointSet = false; 
    }
    private void SearchWalkPoint()
    {
        walkPoint = new Vector3(patrolPoints[currentPoint].transform.position.x, transform.position.y, patrolPoints[currentPoint].transform.position.z);
        walkPointSet = true;
    }
}