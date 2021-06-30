using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Police_CS : MonoBehaviour
{

    public Transform player;

    NavMeshAgent agent;

    Transform spawnPoint;

    public float radius;

    public GameObject goingTo;

    public float chaseSpeed = 8f, patrolSpeed = 5f;

    private DrivingCops carDriver;

    private void Awake()
    {
        carDriver = GetComponent<DrivingCops>();
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPoint = transform;
        StartCoroutine("FindPatrolPoint");
    }

    IEnumerator FindPatrolPoint()
    {
        if (LevelManager_CS.instance.playerhasCrim == true)
        {

            StartCoroutine("ChasePlayer");

        }
        else
        {
            Vector3 moveToPos;
            Vector3 randomPosition = Random.insideUnitSphere * radius;
            //randomPosition += spawnPoint.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, radius, NavMesh.AllAreas))
            {
                moveToPos = hit.position;
            }
            else
            {
                moveToPos = spawnPoint.position;
                //print("Not on NavMesh");
            }
            goingTo.transform.position = moveToPos;
            agent.speed = patrolSpeed;
            StartCoroutine(Patrol(moveToPos));

        }

        yield return null;
    }

    IEnumerator Patrol(Vector3 moveToPos)
    {

        float distance = Vector3.Distance(transform.position, moveToPos);
        agent.SetDestination(moveToPos);
        
        while(distance > agent.stoppingDistance + 13.5f)
        {
            distance = Vector3.Distance(transform.position, moveToPos);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("FindPatrolPoint");
    }

    IEnumerator ChasePlayer()
    {
        while(LevelManager_CS.instance.playerhasCrim == true)
        {
            //print("Chasing!");
            agent.speed = chaseSpeed;
            goingTo.transform.position = player.transform.position;
            agent.SetDestination(player.transform.position);

            yield return new WaitForSeconds(0.2f);
        }

        print("Patrolling!");
        StartCoroutine("FindPatrolPoint");

    }

    void Update()
    {

        float forwardAmount = 0f;
        float turnAmount = 0f;

        float distanceToTarget = Vector3.Distance(transform.position, goingTo.transform.position);
        Vector3 dirToMovePosition = (goingTo.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, dirToMovePosition);

            if (dot > 0)
            {
                // Target in front
                forwardAmount = 1f;

                float stoppingDistance = 30f;
                float stoppingSpeed = 20f;
                if (distanceToTarget < stoppingDistance && carDriver.GetSpeed() > stoppingSpeed)
                {
                    forwardAmount = -1f;
                }
            }
            else
            {
                    forwardAmount = 1f;

            }

            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            if (angleToDir > 0)
            {
                turnAmount = 1f;
            }
            else
            {
                turnAmount = -1f;
            }
        

        carDriver.SetInputs(forwardAmount, turnAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && LevelManager_CS.instance.playerhasCrim)
        {
            LevelManager_CS.instance.ResetPlayerLost();
            LevelManager_CS.instance.playerhasCrim = false;
            GameUI_CS.instance.haveCrim = false;
            GameUI_CS.instance.UpdateUI();
        }
    }
}
