using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickedUp_CS : MonoBehaviour
{
    public static PowerPickedUp_CS instance = null;
    //public GameObject pickupVFX;
    public float speed = 100;
    private void Awake()
    {
        instance = this;

    }

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
    }


}
