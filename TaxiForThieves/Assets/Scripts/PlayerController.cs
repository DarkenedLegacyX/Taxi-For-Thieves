using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    public Rigidbody sphereRB;

    public Transform playerStartPoint;

    public float forwardSpeed = 4f, reverseSpeed = 4f, turnSpeed = 180f, maxSpeed = 50f, gravityForce = 10f;
    public float turnDecreaseValue = 2f;

    private float speedInput, turnInput;
    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .2f;

    public float airDrag = 1f;
    public float groundDrag = 4f;

    void Start()
    {
        instance = this;
        sphereRB.transform.parent = null;
        ResetPosition();
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

        RaycastHit hit;
        grounded = Physics.Raycast(sphereRB.position, -transform.up, out hit, groundRayLength, whatIsGround);
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        turnInput = Input.GetAxis("Horizontal");
        if (grounded)
        {
             if (turnInput != 0)
                 if(speedInput > 0)
                     speedInput -= Mathf.Abs(turnInput) * 1000 * turnDecreaseValue;
                 else if (speedInput < 0)
                     speedInput += Mathf.Abs(turnInput) * 500 * turnDecreaseValue;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnSpeed * Time.deltaTime * speedInput/1000, 0f)); 
        }
        transform.position = sphereRB.transform.position;

    }

    private void FixedUpdate()
    {
        if (grounded)
        {
            sphereRB.drag = groundDrag;
            if (Mathf.Abs(speedInput) > 0)
            {
                sphereRB.AddForce(transform.forward * speedInput);
            }
        } else
        {
            sphereRB.AddForce(Vector3.up * -gravityForce * 100f);
            sphereRB.drag = airDrag;
        }

    }

    public void ResetPosition()
    {
        sphereRB.transform.position = playerStartPoint.position;
        sphereRB.transform.rotation = playerStartPoint.rotation;
        transform.position = sphereRB.transform.position;
        transform.rotation = sphereRB.transform.rotation;
    }

    public void SpeedBoost()
    {
        StartCoroutine(SpeedUpFor(5, 0.33f));
    }

    IEnumerator SpeedUpFor(int speedUpSec, float percentUp)
    {
        float forward = forwardSpeed * percentUp;
        float reverse = reverseSpeed * percentUp;

        forwardSpeed += forward;
        reverseSpeed += reverse;
        yield return new WaitForSecondsRealtime(speedUpSec);
        forwardSpeed -= forward;
        reverseSpeed -= reverse;
    }
}
