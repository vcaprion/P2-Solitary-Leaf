using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]float accelerationPower = 15f;
    [SerializeField]float steeringPower = 2f;
    [SerializeField]bool isAccelerating, isBraking, isDrifting, countownStarted, hasDelivery;
    float steeringAmount, speed, direction;
    [SerializeField]float deliveries, deliveryTime;
    [SerializeField]Text timerText, deliveryText, accelerationText;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isAccelerating = false;
        isBraking = false;
        isDrifting = false;
        deliveries = 0;
        deliveryTime = 60f;
        timerText.text = ($"Time Remaining for next delivery: {deliveryTime} seconds");
        deliveryText.text = ($"Deliveries Completed: {deliveries}");
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
        accelerationText.text = ($"{accelerationPower} mph");
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

        if (Input.GetButtonDown("Escape"))
        {
            Debug.Log ("Game closed");
            Application.Quit();
        }

        if (countownStarted)
        {
            deliveryTime -= Time.deltaTime;
            UpdateCountdown();
        }
    }

    void AccelerationCheck()
    {
        if (isAccelerating)
        {
            accelerationPower += Time.deltaTime*2f;
            if (accelerationPower >= 45)
            {
                accelerationPower = 45;
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
        if(isBraking && accelerationPower > 15)
        {
            accelerationPower -= Time.deltaTime*7.5f;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().tag == "PickupPoint" && deliveries <= 0)
        {
            countownStarted = true;
            hasDelivery = true;
        }
        else if (col.GetComponent<Collider2D>().tag == "PickupPoint" && deliveries > 0)
        {
            hasDelivery = true;
        }

        if (col.GetComponent<Collider2D>().tag == "DropoffPoint" && hasDelivery)
        {
            hasDelivery = false;
            deliveries++;
            UpdateDeliveries();
            deliveryTime += 5;
        }
    }

    void UpdateCountdown()
    {
        timerText.text = ($"Time remaining: {deliveryTime}.");
    }

    void UpdateDeliveries()
    {
        deliveryText.text = ($"Deliveries Completed: {deliveries}.");
    }
}
