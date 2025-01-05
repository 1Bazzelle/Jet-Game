using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float moveSpeed;
    public float maxSpeed;
    public float acceleration;

    public bool accelerate;
    public bool decelerate;
    protected Quaternion curRotation;

    public float pitchUpSpeed;
    protected bool pitchUp;
    Quaternion PitchUp;

    public float pitchDownSpeed;
    protected bool pitchDown;
    Quaternion PitchDown;

    public float tiltSpeed;
    protected bool tiltLeft;
    Quaternion TiltLeft;
    protected bool tiltRight;
    Quaternion TiltRight;

    public float yawSpeed;
    protected bool yawLeft;
    Quaternion YawLeft;
    protected bool yawRight;
    Quaternion YawRight;

    public bool isAlive;
    public bool stillDead;

    public float stallingGravity;
    public bool isStalling;
    private float stallingTimer;

    private bool hasStarted;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 0;
        isAlive = true;
        stillDead = true;
        hasStarted = false;
        rb.isKinematic = true;
        stallingTimer = 0;

    }

    void Update()
    {
        if(isAlive)
        {
            curRotation = transform.rotation;

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(rb.isKinematic && !hasStarted)
                {
                    rb.isKinematic = false;
                    hasStarted = true;
                }
                accelerate = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                accelerate = false;
            }

            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                decelerate = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                decelerate = false;
            }

            if(Input.GetKeyDown(KeyCode.W))
            {
                pitchDown = true;
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                pitchDown = false;
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                pitchUp = true;
            }
            if(Input.GetKeyUp(KeyCode.S))
            {
                pitchUp = false;
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                tiltLeft = true;
            }
            if(Input.GetKeyUp(KeyCode.A))
            {
                tiltLeft = false;
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                tiltRight = true;
            }
            if(Input.GetKeyUp(KeyCode.D))
            {
                tiltRight = false;
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                yawLeft = true;
            }
            if(Input.GetKeyUp(KeyCode.Q))
            {
                yawLeft = false;
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                yawRight = true;
            }
            if(Input.GetKeyUp(KeyCode.E))
            {
                yawRight = false;
            }

            if(moveSpeed < 1000)
            {
                isStalling = true;
                stallingTimer += Time.deltaTime;
                rb.AddForce(Vector3.Lerp(new Vector3(0, 0, 0), Vector3.down * stallingGravity, 0.5f * stallingTimer));
            }
            if(moveSpeed > 1000)
            {
                isStalling = false;
                stallingTimer = 0;
            }
        }
        else if(!isAlive)
        {
            moveSpeed = 0;
            rb.isKinematic = true;
        }
        if(!stillDead)
        {
            transform.position = new Vector3(0f, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            hasStarted = false;
            stillDead = true;
            accelerate = false;
            decelerate = false;
            pitchUp = false;
            pitchDown = false;
            tiltLeft = false;
            tiltRight = false;
            yawLeft = false;
            yawRight = false;
        }
    }

    void FixedUpdate()
    {
        if(accelerate)
        {
            if(moveSpeed < maxSpeed)
            {
                moveSpeed += acceleration;
            }
        }

        if(decelerate)
        {
            if(moveSpeed > 0)
            {
                moveSpeed -= acceleration/3;
            }
        }

        if(pitchDown)
        {
            PitchDown = Quaternion.Euler(pitchDownSpeed, 0, 0);
        }
        if(!pitchDown)
        {
            PitchDown = Quaternion.Euler(0,0,0);
        }

        if(pitchUp)
        {
            PitchUp = Quaternion.Euler(-pitchUpSpeed, 0, 0);
        }
        if(!pitchUp)
        {
            PitchUp = Quaternion.Euler(0,0,0);
        }

        if(tiltLeft)
        {
            TiltLeft = Quaternion.Euler(0, 0, tiltSpeed);
        }
        if(!tiltLeft)
        {
            TiltLeft = Quaternion.Euler(0,0,0);
        }

        if(tiltRight)
        {
            TiltRight = Quaternion.Euler(0, 0, -tiltSpeed);
        }
        if(!tiltRight)
        {
            TiltRight = Quaternion.Euler(0,0,0);
        }

        if(yawLeft)
        {
            YawLeft = Quaternion.Euler(0, -yawSpeed, 0);
        }
        if(!yawLeft)
        {
            YawLeft = Quaternion.Euler(0,0,0);
        }

        if(yawRight)
        {
            YawRight = Quaternion.Euler(0, yawSpeed, 0);
        }
        if(!yawRight)
        {
            YawRight = Quaternion.Euler(0,0,0);
        }

        transform.rotation = curRotation * PitchUp * PitchDown * TiltLeft * TiltRight * YawLeft * YawRight;

        rb.velocity = transform.forward * moveSpeed * Time.fixedDeltaTime;
    }
}
