using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Start()
    {
        
    }
}
