using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconFollow : MonoBehaviour
{
    public Transform playerTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = playerTransform.transform.rotation;
        this.transform.Rotate(90f, 0, 0);
    }
}
