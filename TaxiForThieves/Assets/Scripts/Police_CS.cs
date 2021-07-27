using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Police_CS : MonoBehaviour
{
    [InspectorName("Transform")]

    public Transform player;

    public Transform spawnPoint;

    public Vector3 spawnPLocation;

    [InspectorName("Agent")]

    NavMeshAgent agent;

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
        spawnPLocation = spawnPoint.transform.position;
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
                //if()
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

        while (distance > agent.stoppingDistance + 13.5f)
        {
            distance = Vector3.Distance(transform.position, moveToPos);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("FindPatrolPoint");
    }

    IEnumerator ReturnToSpawn(Vector3 spawnPoint)
    {
        print("Returning to spawn!");
        float distance = Vector3.Distance(transform.position, spawnPoint);
        agent.SetDestination(spawnPoint);
        yield return new WaitForSeconds(10f);
        StartCoroutine("FindPatrolPoint");
    }

    IEnumerator ChasePlayer()
    {
        while (LevelManager_CS.instance.playerhasCrim == true)
        {
            //print("Chasing!");
            agent.speed = chaseSpeed;
            goingTo.transform.position = player.transform.position;
            agent.SetDestination(player.transform.position);

            yield return new WaitForSeconds(0.2f);
        }

        //print("Patrolling!");
        //StartCoroutine("FindPatrolPoint");
        StartCoroutine("ReturnToSpawn", spawnPLocation);

    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && LevelManager_CS.instance.playerhasCrim)
        {
            //StopAllCoroutines();
            StartCoroutine("ReturnToSpawn", spawnPLocation);
            LevelManager_CS.instance.ResetPlayerLost();
            LevelManager_CS.instance.playerhasCrim = false;
            GameUI_CS.instance.haveCrim = false;
        }
    }
}