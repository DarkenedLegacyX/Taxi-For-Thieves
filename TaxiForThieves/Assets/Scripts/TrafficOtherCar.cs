using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficOtherCar : MonoBehaviour
{
    public TrafficWaypoint_CS Car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NpcCar"))
        {
            print("StoppingCar");
            Car.canMove = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NpcCar"))
        {
            StartCoroutine("PleaseMove");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NpcCar"))
        {
            print("StoppingCar");
            Car.canMove = true;
        }
    }

    IEnumerator PleaseMove()
    {
        yield return new WaitForSeconds(5f);
        Car.canMove = true;
    }
}
