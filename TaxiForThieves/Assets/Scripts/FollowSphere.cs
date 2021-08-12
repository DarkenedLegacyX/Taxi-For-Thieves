using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSphere : MonoBehaviour
{
    public Transform player;
    public Transform collider;
    void Start()
    {
    }

    void Update()
    { 
        collider.rotation = player.rotation;
        collider.position = player.position;
    }


}
