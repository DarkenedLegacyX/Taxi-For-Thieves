using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody sphereRB;

    public float forwardSpeed = 4f, reverseSpeed = 4f, turnSpeed = 180f, maxSpeed = 50f, gravityForce = 10f;

    private float speedInput, turnInput;
    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .2f;
    public Transform groundRayPoint;

    void Start()
    {
        sphereRB.transform.parent = null;
    }


    void Update()
    {
        speedInput = 0f;
        if(Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardSpeed * 1000;
        } else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseSpeed * 1000;
        }

        turnInput = Input.GetAxis("Horizontal");
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnSpeed * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

        transform.position = sphereRB.transform.position;
    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit; 

        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;
        }

        if (grounded)
        {
            if (Mathf.Abs(speedInput) > 0)
            {
                sphereRB.AddForce(transform.forward * speedInput);
            }
        } else
        {
            sphereRB.AddForce(Vector3.up * -gravityForce * 100f);
        }

    }
}
