using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_CS : MonoBehaviour
{
    public static LevelManager_CS instance = null;

    public Transform[] spawns;
    public GameObject crim;
    public bool playerhasCrim;

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

        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
