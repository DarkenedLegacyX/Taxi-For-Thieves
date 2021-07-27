using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine_CS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (LevelManager_CS.instance.playerhasCrim == true)
            {
                print("Criminal Dropped off!");
                LevelManager_CS.instance.playerhasCrim = false;
                GameUI_CS.instance.haveCrim = false;
                //gameObject.transform.position = new Vector3(0, -25, 0);
            }
            else
            {
                print("Pick up dem Crims!");
            }
        }
    }
}
