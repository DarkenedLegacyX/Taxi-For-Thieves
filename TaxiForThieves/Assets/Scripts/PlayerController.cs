using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    public Rigidbody sphereRB;

    public Transform playerStartPoint;
    public Transform rayCastPoint;
    public GameObject indicator;
    public Vector3 indicatorTarget;

    public float turnSpeed = 180f, maxSpeed = 8f, maxReverseSpeed = 4f, gravityForce = 10f;
    public float turnDecreaseValue = 2f;
    public float velocity;

    public float speedInput, turnInput, speedPenalty;
    bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .1f;

    public float airDrag = 1f;
    public float groundDrag = 4f;

    public bool isBoosted = false;

    void Start()
    {
        instance = this;
        sphereRB.transform.parent = null;
        ResetPosition();
        indicatorTarget = new Vector3(0, 0, 0);
    }


    void Update()
    {

        if (isBoosted == true)
        {
            //print("ZOOMING");
            maxSpeed = 15;
        }
        else {
            //print("Turtle Speed Time!");
            maxSpeed = 8;
        }


        velocity = sphereRB.velocity.magnitude;

        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * maxSpeed;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * maxReverseSpeed;
        }

        RaycastHit hit;
        grounded = Physics.Raycast(rayCastPoint.position, -transform.up, out hit, groundRayLength, whatIsGround);
                
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        turnInput = Input.GetAxis("Horizontal");
        if (grounded)
        {
            speedPenalty = 1 - (Mathf.Abs(turnInput) * turnDecreaseValue * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnSpeed * Time.deltaTime * velocity, 0f)); 
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
                sphereRB.AddForce(transform.forward * speedInput * 1000 * speedPenalty);
            }
        } else
        {
            sphereRB.AddForce(Vector3.up * -gravityForce * 100f);
            sphereRB.drag = airDrag;
        }

        if(LevelManager_CS.instance.playerhasCrim)
        {
            Vector3 direction = indicatorTarget - indicator.transform.position;
            indicator.transform.rotation = Quaternion.Lerp(indicator.transform.rotation, Quaternion.LookRotation(direction), 2.0f * Time.deltaTime);
        }
    }

    public void ResetPosition()
    {
        sphereRB.transform.position = playerStartPoint.position;
        sphereRB.transform.rotation = playerStartPoint.rotation;
        transform.position = sphereRB.transform.position;
        transform.rotation = sphereRB.transform.rotation;
    }

    public void SpeedBoost(int time)
    {
        isBoosted = true;
        StartCoroutine(SpeedUpFor(time, 0.33f));
    }
    public void ActivateIndicator(bool activate)
    {
        indicator.SetActive(activate);
    }

    IEnumerator SpeedUpFor(int speedUpSec, float percentUp)
    {
        //float forward = forwardSpeed * percentUp;
        //float reverse = reverseSpeed * percentUp;

        //forwardSpeed += forward;
        //reverseSpeed += reverse;
        yield return new WaitForSecondsRealtime(speedUpSec);
        print("Turning off boost");
        isBoosted = false;
        //forwardSpeed -= forward;
        //reverseSpeed -= reverse;
    }
}
