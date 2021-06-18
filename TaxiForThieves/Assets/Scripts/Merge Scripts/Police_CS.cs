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
            randomPosition += spawnPoint.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, radius, NavMesh.AllAreas))
            {
                moveToPos = hit.position;
            }
            else
            {
                moveToPos = spawnPoint.position;
                print("Not on NavMesh");
            }
            goingTo.transform.position = moveToPos;
            agent.speed = patrolSpeed;
            StartCoroutine(Patrol(moveToPos));

        }

        /* NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(moveToPos, path);

        goingTo.transform.position = moveToPos;
        if(path.status == NavMeshPathStatus.PathComplete)
        {
            StartCoroutine(Patrol(moveToPos));

        }
        else
        {

            print("Reolling");
            StartCoroutine("FindPatrolPoint");
        }*/

        yield return null;
    }

    IEnumerator Patrol(Vector3 moveToPos)
    {

        float distance = Vector3.Distance(transform.position, moveToPos);
        agent.SetDestination(moveToPos);
        
        while(distance > agent.stoppingDistance + 0.5f)
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
            //agent.SetDestination(player.transform.position);

     }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && LevelManager_CS.instance.playerhasCrim)
        {
            LevelManager_CS.instance.ResetPlayerLost();
            LevelManager_CS.instance.playerhasCrim = false;
            LevelManager_CS.instance.SpawnACrim();
            GameUI_CS.instance.haveCrim = false;
            GameUI_CS.instance.UpdateUI();
        }
    }
}
