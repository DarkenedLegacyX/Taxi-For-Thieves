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
            Police_CS.instance.chaseSpeed = 7;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cop"))
        {
            print("SPEEDING UP");
            Police_CS.instance.chaseSpeed = 14;
        }

    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10f);
        print("SPEEDING BACK UP AFTER DESTORY");
        Police_CS.instance.chaseSpeed = 14;
        Object.Destroy(gameObject);
    }
}

