using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_CS : MonoBehaviour
{
    public static Powerup_CS instance = null;
    public bool isCollected = false;
    public int ranNum;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isCollected == false)
            {
                ranNum = Random.Range(1, 101);
                if(ranNum >= 1 && ranNum <= 20)
                {
                    print("Disguise!");
                    LevelManager_CS.instance.disguisePower = true;
                    Destroy(gameObject);
                }
                else if(ranNum >= 21 && ranNum <= 60)
                {
                    print("Speed!");
                    LevelManager_CS.instance.speedPower = true;
                    Destroy(gameObject);
                }
                else if (ranNum >= 61 && ranNum <= 100)
                {
                    print("Mud!");
                    LevelManager_CS.instance.mudPower = true;
                    Destroy(gameObject);
                }
                else
                {
                    print("What?");
                    Destroy(gameObject);
                }
                isCollected = true;
            }
            else
            {
                print("Powerup, already picked up!");
            }

            
        }
    }
}