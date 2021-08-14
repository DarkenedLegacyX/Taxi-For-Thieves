using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud_CS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Destroy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cop"))
        {
            print("SLOWING DOWN");
            other.SendMessage("SetChaseSpeed", 6);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cop"))
        {
            print("SPEEDING UP");
            other.SendMessage("SetChaseSpeed", 15);
        }

    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10f);
        print("SPEEDING BACK UP AFTER DESTORY");
        LevelManager_CS.instance.SetAllPoliceChaseSpeed(16);
        Object.Destroy(gameObject);
    }
}

