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
    public bool gameOver;
    float steeringAmount, speed, direction;
    [SerializeField]float deliveries, deliveryTime;
    [SerializeField]Text timerText, deliveryText, accelerationText, hasDeliveryText;
    [SerializeField]GameObject gameOverMenu;

    public int randomNumber;
    private int lastNumber;
    public List<GameObject> PickupPoints = new List<GameObject>();
    public List<GameObject> DropoffPoints = new List<GameObject>();
    private GameObject currentPPoint;
    private GameObject currentDpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        NewRandomNumber();
        rb = GetComponent<Rigidbody2D>();
        isAccelerating = false;
        isBraking = false;
        isDrifting = false;
        deliveries = 0;
        deliveryTime = 60f;
        timerText.text = ($"Time Remaining for next delivery: {deliveryTime} seconds");
        deliveryText.text = ($"Deliveries Completed: {deliveries}");
        hasDeliveryText.text = ("Pickup the food");
        gameOver = false;
        gameOverMenu.SetActive(false);
        Time.timeScale = 1;
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
                if (randomNumber >= 0)
        {
                foreach (GameObject obj in PickupPoints)
            {
                obj.SetActive(false);
            }
                foreach (GameObject obj in DropoffPoints)
            {
                obj.SetActive(false);
            }
            PickupPoints[randomNumber].SetActive(true);
            DropoffPoints[randomNumber].SetActive(true);
        }



        accelerationText.text = ($"{accelerationPower.ToString("F1")} mph");
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
            isDrifting = false;
            BrakeCheck();
            DriftCheck();
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

        if (deliveryTime <= 0)
        {
            countownStarted = false;
            UpdateCountdown();
            gameOver = true;
            gameOverMenu.SetActive(true);
            speed = 0;
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
            accelerationPower -= Time.deltaTime*1.5f;
        }
        else
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
            hasDeliveryText.text = "You have a delivery!";
        }
        else if (col.GetComponent<Collider2D>().tag == "PickupPoint" && deliveries > 0)
        {
            hasDelivery = true;
            hasDeliveryText.text = "You have a delivery!";
        }

        if (col.GetComponent<Collider2D>().tag == "DropoffPoint" && hasDelivery)
        {
            hasDelivery = false;
            hasDeliveryText.text = "Pick up the next delivery!";
            deliveries++;
            UpdateDeliveries();
            deliveryTime += 5;
        }
    }

    void UpdateCountdown()
    {
        timerText.text = ($"Time remaining: {deliveryTime.ToString("F1")}.");
    }

    void UpdateDeliveries()
    {
        deliveryText.text = ($"Deliveries Completed: {deliveries.ToString("F1")}.");
    }

    public virtual void NewRandomNumber()
{
    randomNumber = Random.Range(0, 2);
    if (randomNumber == lastNumber)
    {
        randomNumber = Random.Range(0, 2);
    }
    lastNumber = randomNumber;
}
}
