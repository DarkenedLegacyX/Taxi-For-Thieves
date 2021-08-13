using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCar : MonoBehaviour
{
    public GameObject car;
    

    public void StopNPC(bool stop)
    {
        car.SendMessage("SetCanMove", stop);
    }
}
