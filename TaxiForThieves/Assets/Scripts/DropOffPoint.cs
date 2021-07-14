using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffPoint : MonoBehaviour
{
    GameObject sphere;
    Collider dropOffCollider;

    void Start()
    {
        sphere = this.transform.GetChild(0).gameObject;
        dropOffCollider = GetComponent<Collider>();
    }

    void Update()
    {
        
    }

    public void Activate()
    {
        sphere.SetActive(true);
        dropOffCollider.enabled = true;
    }

    public void Deactivate()
    {
        sphere.SetActive(false);
        dropOffCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (LevelManager_CS.instance.playerhasCrim == true)
            {
                print("Criminal Dropped off!");
                Deactivate();
                LevelManager_CS.instance.playerhasCrim = false;
                //PlayerController.instance.ActivateIndicator(false);
                LevelManager_CS.instance.SpawnACrim();
                GameUI_CS.instance.haveCrim = false;
                GameUI_CS.instance.UpdateUI();
            }
            else
            {
                print("Pick up dem Crims!");
            }
        }
    }
}
