using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]float accelerationPower = 15f;
    [SerializeField]float steeringPower = 2f;
    [SerializeField]bool isAccelerating, isBraking, isDrifting;
    float steeringAmount, speed, direction;
    [SerializeField]float deliveries, deliveryTime;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isAccelerating = false;
        isBraking = false;
        isDrifting = false;
        deliveries = 0;
        deliveryTime = 60f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        steeringAmount = -Input.GetAxis ("Horizontal");
        speed = Input.GetAxis("Vertical")*accelerationPower;
        direction = Mathf.Sign(Vector2.Dot (rb.velocity, rb.GetRelativeVector(Vector2.up)));
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;

        rb.AddRelativeForce (Vector2.up * speed);

        rb.AddRelativeForce (-Vector2.right * rb.velocity.magnitude *steeringAmount/2);
    }

    void Update()
    {
        if (Input.GetButton("Accelerate"))
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
        }
        AccelerationCheck();

        if (Input.GetButton("Brake"))
        {
            isBraking = true;
            BrakeCheck();
            if (isAccelerating)
            {
                isDrifting = true;
                isBraking = false;
                DriftCheck();
            }
        }
        else
        {
            isBraking = false;
        }
    }

    void AccelerationCheck()
    {
        if (isAccelerating)
        {
            accelerationPower += Time.deltaTime*2f;
            if (accelerationPower >= 60)
            {
                accelerationPower = 60;
                return;
            }
        }
        else if (!isAccelerating && accelerationPower > 15)
        {
            accelerationPower -= Time.deltaTime*5f;
        }
        else
        {
            accelerationPower = 15;
        }
    }

    void BrakeCheck()
    {
        if(isBraking)
        {
            accelerationPower = 10f;
        }
        else
        {
            return;
        }
    }

    void DriftCheck()
    {
        if(isDrifting)
        {
            steeringPower = 3f;
        }
        if (!isAccelerating)
        {
            steeringPower = 2f;
            isDrifting = false;
        }
    }
}
