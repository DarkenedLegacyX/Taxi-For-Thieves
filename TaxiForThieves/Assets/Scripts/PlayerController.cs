using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    public Rigidbody sphereRB;

    public Transform playerStartPoint;
    public Transform rayCastPoint;
    public Transform wheelFL, wheelFR, wheelBL, wheelBR;
    public GameObject indicator;
    public Vector3 indicatorTarget;

    public float turnSpeed = 180f, maxSpeed = 8f, maxReverseSpeed = 4f, gravityForce = 10f;
    public float turnDecreaseValue = 2f;
    public float velocity;

    public float speedInput, turnInput, speedPenalty;
    public GameObject loot;
    public GameObject poofPolice;
    bool grounded;
    bool holdPlayer;

    [Header("POWERUP")]
    public GameObject mudObject;
    public bool mudPower;
    public bool disguisePower;
    public MeshRenderer[] playerCarMesh;
    public bool speedPower;

    public LayerMask whatIsGround;
    public float groundRayLength = .1f;

    public float airDrag = 1f;
    public float groundDrag = 4f;

    public bool isBoosted = false;
    public GameObject trail;


    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}

        //DontDestroyOnLoad(gameObject);
        instance = this;
    }
    void Start()
    {
        sphereRB.transform.parent = null;
        ResetPosition();
        //indicatorTarget = new Vector3(0, 0, 0);
        holdPlayer = false;
    }


    void Update()
    {

        if (isBoosted == true)
        {
            //print("ZOOMING");
            maxSpeed = 23;
            trail.GetComponent<TrailRenderer>().time = 0.2f;
        }
        else
        {
            //print("Turtle Speed Time!");
            maxSpeed = 12;
            trail.GetComponent<TrailRenderer>().time = 0f;
        }

        velocity = sphereRB.velocity.magnitude;

        speedInput = 0f;
        if(!holdPlayer)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                speedInput = Input.GetAxis("Vertical") * maxSpeed;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                speedInput = Input.GetAxis("Vertical") * maxReverseSpeed;
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                StartCoroutine("HoldPlayer");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (mudPower == true)
                {

                    Instantiate(mudObject, this.transform.position, this.transform.rotation);
                    print("Used mud!");
                    Powerup_CS.instance.isCollected = false;
                    mudPower = false;
                    //StartCoroutine("ActivateDisguise");
                }
                if (speedPower == true)
                {
                    //Instantiate(mudObject, this.transform.position, this.transform.rotation);
                    print("Used speed!");
                    PlayerController.instance.SpeedBoost(5);
                    Powerup_CS.instance.isCollected = false;
                    speedPower = false;
                    //StartCoroutine("ActivateDisguise");
                }
                if (disguisePower == true && LevelManager_CS.instance.playerhasCrim == true)
                {
                    //Instantiate(mudObject, this.transform.position, this.transform.rotation);
                    print("Used Diguise!");
                    Powerup_CS.instance.isCollected = false;
                    disguisePower = false;
                    StartCoroutine("ActivateDisguise");
                }
                else if (disguisePower == true && LevelManager_CS.instance.playerhasCrim == false)
                {
                    print("Cops must chase you!");
                }
                else
                {
                    print("No Powa");
                }
            }

        }
        RaycastHit hit;
        grounded = Physics.Raycast(rayCastPoint.position, -transform.up, out hit, groundRayLength, whatIsGround);

        turnInput = Input.GetAxis("Horizontal");
        if (grounded && !holdPlayer)
        {
            if (Mathf.Abs(turnInput) > 0.1f && speedInput > 0)
            {
                if (speedPenalty > 0.75f)
                    speedPenalty -= (turnDecreaseValue * Time.deltaTime);
            }
            else
                speedPenalty = 1;

            //print(Mathf.Abs(turnInput) + " : " + speedPenalty);

            if (speedInput < 0)
                transform.localEulerAngles += new Vector3(0f, -turnInput * turnSpeed * Time.deltaTime * velocity, 0f);
            else
                transform.localEulerAngles += new Vector3(0f, turnInput * turnSpeed * Time.deltaTime * velocity, 0f);
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
                sphereRB.AddForce(((transform.forward * speedInput) * 1000) * speedPenalty);
            }
        }
        else
        {
            sphereRB.AddForce(Vector3.up * -gravityForce * 100f);
            sphereRB.drag = airDrag;
        }

        if (LevelManager_CS.instance.playerhasCrim)
        {
            Vector3 direction = indicatorTarget - indicator.transform.position;
            float dist = Vector3.Distance(indicatorTarget, indicator.transform.position);
            if(dist < 40)
            {
                indicator.SetActive(false);
            }
            else
            {
                indicator.SetActive(true);
            }
            indicator.transform.rotation = Quaternion.Lerp(indicator.transform.rotation, Quaternion.LookRotation(direction), 100.0f * Time.deltaTime);
        }
        else
        {
            //print("1111111");
            Vector3 direction = indicatorTarget - indicator.transform.position;
            float dist = Vector3.Distance(indicatorTarget, indicator.transform.position);
            if (dist < 40)
            {
                indicator.SetActive(false);
            }
            else
            {
                indicator.SetActive(true);
            }
            //indicator.transform.rotation = Quaternion.Lerp(indicator.transform.rotation, Quaternion.LookRotation(direction), 100.0f * Time.deltaTime);
            indicator.transform.rotation = Quaternion.LookRotation(direction);
        }

        
        

        if (speedInput < 0)
        {
            wheelFL.Rotate(-velocity * 360 * Time.deltaTime, 0, 0);
            wheelFR.Rotate(-velocity * 360 * Time.deltaTime, 0, 0);
            wheelBL.Rotate(-velocity * 360 * Time.deltaTime, 0, 0);
            wheelBR.Rotate(-velocity * 360 * Time.deltaTime, 0, 0);
        }
        else
        {
            wheelFL.Rotate(velocity * 360 * Time.deltaTime, 0, 0);
            wheelFR.Rotate(velocity * 360 * Time.deltaTime, 0, 0);
            wheelBL.Rotate(velocity * 360 * Time.deltaTime, 0, 0);
            wheelBR.Rotate(velocity * 360 * Time.deltaTime, 0, 0);
        }

        wheelFL.parent.transform.localEulerAngles = new Vector3(0, turnInput * 360 * 5 * Time.deltaTime, 0);
        wheelFR.parent.transform.localEulerAngles = new Vector3(0, turnInput * 360 * 5 * Time.deltaTime, 0);

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

    public void DropSomeLoot(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            StartCoroutine("DropLoot");
        }
    }

    IEnumerator DropLoot()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0f, 1f));
        Instantiate(loot, transform.position + new Vector3(0, 1, -0.5f), transform.rotation, this.transform);
        yield return null;
    }

    IEnumerator ActivateDisguise()
    {
        LevelManager_CS.instance.playerhasCrim = false;
        
        for(int i = 0; i < playerCarMesh.Length; i++)
        {
            playerCarMesh[i].enabled = false;
        }

        float disguiseTime = 10;
        while (disguiseTime >= 1)
        {
            yield return new WaitForSeconds(1);

            disguiseTime--;
            //Converting timer to minutes and seconds
            float seconds = Mathf.FloorToInt(disguiseTime % 60);

            //print(currentTime);

            //Making a String Format.
            GameUI_CS.instance.disguiseTimer.text = seconds.ToString();
        }

        for (int i = 0; i < playerCarMesh.Length; i++)
        {
            playerCarMesh[i].enabled = true;
        }
        //yield return new WaitForSecondsRealtime(10f);
        LevelManager_CS.instance.playerhasCrim = true;
    }

    public IEnumerator HoldPlayer()
    {
        holdPlayer = true;
        yield return new WaitForSecondsRealtime(3);
        holdPlayer = false;
    }

    public void PoofPoliceGotUs()
    {
        poofPolice.SetActive(true);
    }
}