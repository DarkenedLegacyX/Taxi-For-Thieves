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
            if (isFloor == true) {
                print("Floor Booooooooooooooost!");
                PlayerController.instance.SpeedBoost(1);
            }
            else
            {
                print("Powerup Booooooost!");
                PlayerController.instance.SpeedBoost(5);
                Destroy(gameObject);
            }
        }
    }
}
