using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficWaypoint_CS : MonoBehaviour
{
    // put the points from unity interface
    public Transform[] wayPointList;
    public int lastWaypoint;
    public int currentWayPoint = 0;
    Transform targetWayPoint;

    public float speed;

    public bool canMove = true;


    // Update is called once per frame
    void Update()
    {

        // check if we have somewere to walk
        if (currentWayPoint < this.wayPointList.Length && canMove == true)
        {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];
            walk();
        }
    }

    public void walk()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            if (currentWayPoint == lastWaypoint)
            {
                //print("Resetting Route");
                currentWayPoint = 0;
                targetWayPoint = wayPointList[currentWayPoint];
            }
            else
            {
                targetWayPoint = wayPointList[currentWayPoint];
            }

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            StartCoroutine("PauseWaypoint");
        }
    }

    IEnumerator PauseWaypoint()
    {
        print("Crash!");
        canMove = false;
        yield return new WaitForSecondsRealtime(5f);
        canMove = true;
    }
}
