using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRadar : MonoBehaviour
{
    public GameObject radar;
    private Animator animator;

    void Start()
    {
        animator = radar.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Rotate(0,0,1 * Time.deltaTime);
        if(LevelManager_CS.instance.playerhasCrim == true)
        {
            print("Test");
            animator.SetBool("HasCrim", true);
        }
        else
        {
            animator.SetBool("HasCrim", false);

        }
    }
}
