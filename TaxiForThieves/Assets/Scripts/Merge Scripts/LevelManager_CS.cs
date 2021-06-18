using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_CS : MonoBehaviour
{
    public static LevelManager_CS instance = null;

    public Transform[] spawns;
    public GameObject crim;
    public bool playerhasCrim;

    public GameObject[] dropOffPoints;

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

    void Start()
    {
 
    }

    void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void SpawnACrim()
    {

    }

    public GameObject GetRandomDropOff()
    {
        int rand = Random.Range(0, (dropOffPoints.Length - 1));
        return dropOffPoints[rand];
    }
}
