using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal_CS : MonoBehaviour
{
    Rigidbody rb;
    GameObject dropOffPoint;
    public GameObject model1, model2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dropOffPoint = LevelManager_CS.instance.GetDropOff();

        if (LevelManager_CS.instance.crimModel)
        {
            model1.SetActive(true);
            model2.SetActive(false);
        }
        else
        {
            model2.SetActive(true);
            model1.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            print("Criminal Picked up!");
            LevelManager_CS.instance.CrimPickedUp();
            
            dropOffPoint.SendMessage("Activate");
            //PlayerController.instance.ActivateIndicator(true);
            gameObject.SetActive(false);

            //gameObject.transform.position = new Vector3(0, 75, 0);
        }
    }
}
