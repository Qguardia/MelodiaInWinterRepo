﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class NavmeshAgentScript : MonoBehaviour
{
    // THIS SCRIPT IS ALL ABOUT SETTING MOVEMENT ORDERS TO A GUARD. 

    public Transform target; //This is the player's body's transform
    public GameObject player; 
    NavMeshAgent agent;
    public GameObject[] wayPoints;

    private Transform currentDestination;
    private int PatrolPoint;
    private int PatrolPointCount;
    private float dist;
    private float seenDist;
    public int AIState;

    public float patrolSpeed;
    public float chaseSpeed;

    public Vector3 guardPosition;
    public float sightRange;
    //public bool inLoS; // NOT USED? Delete?
    private bool hadChased;
    public Vector3 lastSeenAt;
    public float delay = 3f;
    public float patrolCheckRange;

    public bool jobIsPatrol;
    public bool jobIsStandGaurd;

    public Quaternion initialRotation;
    public bool hasResetRotation;

    //Coin boolean

    public bool coinHeard;
    public Transform Coin;

    public float InvestigateSpeed;
    //Deaf/stunned

    public bool isStunned;

    public long test;

    // This enemy uses an integer to flag the AI state:

    // 1 = Head to the player and raycast to check LOS again
    // 2 = Head to player's last known location.
    // 3 = Patrol
    // To Be Implemented options next...
    // 4 = Stand at fixed position and rotation
    // 5 = Return to fixed position


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("PlayerBody").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        PatrolPoint = 0;
        PatrolPointCount = wayPoints.Length;
        patrolCheckRange = 1.2f;
        coinHeard = false;


        if (!jobIsPatrol && !jobIsStandGaurd)
        {
            Debug.LogError(gameObject + " has no job set!");
            Debug.Break();
        }

        if (jobIsPatrol && jobIsStandGaurd)
        {
            Debug.LogError(gameObject + " has TWO jobs set! You must pick one only");
            Debug.Break();
        }

        if (wayPoints[0] == null)
        {
            Debug.LogError(gameObject + " has no starting waypoint! You MUST set one!");
            Debug.Break();
        }

        ResetJobState();
    }

    public void ResetJobState()
    {
        if (jobIsStandGaurd)
        {
            AIState = 4;
            transform.position = wayPoints[0].transform.position;
            transform.rotation = wayPoints[0].transform.rotation;
        }

        if (jobIsPatrol)
        {
            AIState = 3;
        }
    }

    void DelayedSwitch()
    {
        // This returns the guard to its job.

        if (jobIsPatrol)
        {
            AIState = 3;
        }
        else
        {
            AIState = 5;
        }

    }

    // Update is called once per frame
    void Update()
    {
        guardPosition = transform.position;

        if (AIState == 8)
        {
            //Debug.Log("Hit Registered");

            if (isStunned == true)
            {
                DeafenedState();
               // Debug.Log("Target deafened");
            }
            
            else
            {
                DelayedSwitch();
                ResetJobState();
            }
            return;
        }

        if (AIState == 1)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.position);
            lastSeenAt = target.transform.position;

            if (player.GetComponent<PlayerHealth>().playerIsAlive == false) // If player is dead, AI goes to patrol
            {
                AIState = 3;
            }
        }

        if (AIState == 2) // HEAD TO LAST PLACE PLAYER WAS SEEN 
        {
            agent.speed = chaseSpeed;
            seenDist = Vector3.Distance(lastSeenAt, guardPosition);
            if (seenDist > 0.5)
            {
                Invoke("TryToFireGun", delay);
                agent.SetDestination(lastSeenAt);
            }
            else if (seenDist <= 0.5)
            {
                Invoke("DelayedSwitch", delay);
                seenDist = 100;
            }
        }

        if (AIState == 3) // ON PATROL -- THIS ALL WORKS AS DESIRED. 
        {
            agent.speed = patrolSpeed;
            currentDestination = wayPoints[PatrolPoint].transform;
            dist = Vector3.Distance(currentDestination.position, transform.position);
            //Debug.Log("No of points: " + PatrolPointCount + " Current: " + PatrolPoint);
           // Debug.Log("HI"); 
            if (dist > patrolCheckRange)
            {
                agent.SetDestination(currentDestination.position);
            }
            else if (dist <= patrolCheckRange && PatrolPoint == (PatrolPointCount - 1))
            {
                PatrolPoint = 0;
                DelayedSwitch();
            }

            else if (dist <= patrolCheckRange && PatrolPoint < (PatrolPointCount - 1))
            {
                PatrolPoint++;
                DelayedSwitch();
            }
        }

        if (AIState == 4) // GUARD WHERE YOU ARE.
        {
            // NOTHING!
        }

        if (AIState == 5) // RETURN TO GUARD STATION
        {
            agent.speed = patrolSpeed;
            currentDestination = wayPoints[0].transform;
            dist = Vector3.Distance(currentDestination.position, transform.position);
            if (dist > patrolCheckRange)
            {
                agent.SetDestination(currentDestination.position);
            }
            else
            {
                gameObject.transform.position = wayPoints[0].transform.position;
                gameObject.transform.rotation = wayPoints[0].transform.rotation;
                AIState = 4;
            }
        }

        if (AIState == 6)
        {
            // Set up to head to a location given by an alarm or something. Needs a 'Last Seen At'
        }

        if (AIState == 7) //DistractionByCoin
        {
            hasResetRotation = false;
            agent.speed = InvestigateSpeed;
            //agent.SetDestination(target.position);
            if (coinHeard == true)
            {
                transform.LookAt(GameObject.FindGameObjectWithTag("Coin").transform);
                StartCoroutine(RotateInital());
                coinHeard = false;
            }

            // StartCoroutine(RotateInital());
        }
    }

    private IEnumerator RotateInital()
    {
        yield return new WaitForSeconds(5);
        /*   if (AIState == 4)
           {
               transform.rotation = initialRotation;
           }*/
        DelayedSwitch();
        transform.rotation = initialRotation;
    }
    private IEnumerator DeafenedState()
    {
        agent.speed = patrolSpeed - patrolSpeed;
        yield return new WaitForSeconds(8);
        isStunned = false;
    }
}
