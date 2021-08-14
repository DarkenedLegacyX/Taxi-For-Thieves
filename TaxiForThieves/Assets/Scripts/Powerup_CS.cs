using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_CS : MonoBehaviour
{
    public static Powerup_CS instance = null;
    public GameObject pickupVFX;
    public GameObject crimVFX;
    public GameObject crimDropVFX;
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
        if (other.CompareTag("FinishLine"))
        {
            crimDropVFX.transform.position = other.transform.position;
            print("Playing!");
            StartCoroutine("ResetCrimDropVFX");
        }

        if (other.CompareTag("Crim"))
        {
            crimVFX.transform.position = other.transform.position;
            StartCoroutine("ResetCrimVFX");
        }

        if (other.CompareTag("Powerup"))
        {
            pickupVFX.transform.position = other.transform.position;
            if (isCollected == false)
            {
                ranNum = Random.Range(1, 101);
                if (ranNum >= 1 && ranNum <= 20)
                {                    
                    print("Disguise!");
                    
                    GameUI_CS.instance.disguiseIMG.SetActive(true);
                    PlayerController.instance.disguisePower = true;
                    //Destroy(gameObject);
                    //PowerPickedUp_CS.instance.StartCoroutine("PlayEffects");
                    StartCoroutine(Respawn(other));
                    StartCoroutine("ResetPowerUpVFX");
                }
                else if (ranNum >= 21 && ranNum <= 60)
                {
                    print("Speed!");
                    GameUI_CS.instance.speedIMG.SetActive(true);
                    PlayerController.instance.speedPower = true;
                    //Destroy(gameObject);
                    //PowerPickedUp_CS.instance.StartCoroutine("PlayEffects");
                    StartCoroutine(Respawn(other));
                    StartCoroutine("ResetPowerUpVFX");
                }
                else if (ranNum >= 61 && ranNum <= 100)
                {
                    print("Mud!");
                    GameUI_CS.instance.mudIMG.SetActive(true);
                    PlayerController.instance.mudPower = true;
                    //Destroy(gameObject);
                    //PowerPickedUp_CS.instance.StartCoroutine("PlayEffects");
                    StartCoroutine(Respawn(other));
                    StartCoroutine("ResetPowerUpVFX");
                }
                else
                {
                    print("What?");
                    //Destroy(gameObject);
                    //PowerPickedUp_CS.instance.StartCoroutine("PlayEffects");
                    StartCoroutine(Respawn(other));
                    StartCoroutine("ResetPowerUpVFX");
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
        //print("Off!");
        yield return new WaitForSeconds(30f);
        //print("On!");
        other.gameObject.SetActive(true);
    }

    IEnumerator ResetPowerUpVFX()
    {
        pickupVFX.SetActive(true);
        print("POWER VFX ON!");
        yield return new WaitForSeconds(2f);
        pickupVFX.SetActive(false);
        print("POWER VFX OFF!");
    }

    IEnumerator ResetCrimVFX()
    {
        crimVFX.SetActive(true);
        print("CRIM VFX ON!");
        yield return new WaitForSeconds(2f);
        crimVFX.SetActive(false);
        print("CRIM VFX OFF!");
    }

    IEnumerator ResetCrimDropVFX()
    {
        crimDropVFX.SetActive(true);
        print("CRIM DROP VFX ON!");
        yield return new WaitForSeconds(2f);
        crimDropVFX.SetActive(false);
        print("CRIM DROP VFX OFF!");
    }


}