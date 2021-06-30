using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CS : MonoBehaviour
{
    Rigidbody rb;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal * runSpeed,0 , vertical * runSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            if (LevelManager_CS.instance.playerhasCrim == true)
            {
                print("Criminal Dropped off!");
                LevelManager_CS.instance.playerhasCrim = false;
                GameUI_CS.instance.haveCrim = false;
                GameUI_CS.instance.UpdateUI();
                //gameObject.transform.position = new Vector3(0, -25, 0);
            }
            else
            {
                print("Pick up dem Crims!");
            }
        }
    }

}
