using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public bool isFloor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFloor == true) 
            {
                print("Floor Booooooooooooooost!");
                if (PlayerController.instance.isBoosted == false)
                {
                    PlayerController.instance.SpeedBoost(1);
                }
            }
        }
    }
}
