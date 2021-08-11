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

        if (other.CompareTag("Powerup"))
        {
            if (isCollected == false)
            {
                ranNum = Random.Range(1, 101);
                if (ranNum >= 1 && ranNum <= 20)
                {
                    print("Disguise!");
                    PlayerController.instance.disguisePower = true;
                    //Destroy(gameObject);
                    PowerPickedUp_CS.instance.StartCoroutine("PlayEffects");
                    StartCoroutine(Respawn(other));
                }
                else if (ranNum >= 21 && ranNum <= 60)
                {
                    print("Speed!");
                    PlayerController.instance.speedPower = true;
                    //Destroy(gameObject);
                    PowerPickedUp_CS.instance.StartCoroutine("PlayEffects");
                    StartCoroutine(Respawn(other));
                }
                else if (ranNum >= 61 && ranNum <= 100)
                {
                    print("Mud!");
                    PlayerController.instance.mudPower = true;
                    //Destroy(gameObject);
                    PowerPickedUp_CS.instance.StartCoroutine("PlayEffects");
                    StartCoroutine(Respawn(other));
                }
                else
                {
                    print("What?");
                    //Destroy(gameObject);
                    PowerPickedUp_CS.instance.StartCoroutine("PlayEffects");
                    StartCoroutine(Respawn(other));
                }
                isCollected = true;
            }
            else
            {
                print("Powerup, already picked up!");
            }


        }
    }
    IEnumerator Respawn(Collider other)
    {
        other.gameObject.SetActive(false);
        print("Off!");
        yield return new WaitForSeconds(30f);
        print("On!");
        other.gameObject.SetActive(true);
    }
}