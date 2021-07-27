using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal_CS : MonoBehaviour
{
    Rigidbody rb;
    GameObject dropOffPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dropOffPoint = LevelManager_CS.instance.GetRandomDropOff();
            print("Criminal Picked up!");
            LevelManager_CS.instance.CrimPickedUp();
            gameObject.transform.position = new Vector3(0, 75, 0);
            dropOffPoint.SendMessage("Activate");
            //PlayerController.instance.ActivateIndicator(true);
            gameObject.SetActive(false);
        }
    }
}
