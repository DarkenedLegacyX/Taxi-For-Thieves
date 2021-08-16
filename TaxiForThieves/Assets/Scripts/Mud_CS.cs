using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud_CS : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(DestroyMud());
        
    }




    IEnumerator DestroyMud()
    {
        yield return new WaitForSeconds(10f);
        print("SPEEDING BACK UP AFTER DESTORY");
        LevelManager_CS.instance.isOnMud = false;
        LevelManager_CS.instance.SetAllPoliceChaseSpeed(16);

        Destroy(gameObject);
    }
}

