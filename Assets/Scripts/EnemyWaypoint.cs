using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    public List<Transform> PatrolPoints = new List<Transform>();
    private Transform target;
    // public UnityEngine.AI.NavMeshAgent ai;
    private bool atDestination;
    public int patrolNum;
    public float Speed;
    private GameObject BackOfCar;
    private bool isStop;
    public float stopTime;
    private int currentWaypoint = 0;
    public float waypointDistance = 3f;
    public float startSpeed;
    public Transform Reverse;
    private Transform wp;
    private bool CarInFront;
    private Transform TargetPos;
    public float RotationSpeed;
    private Transform rotat;

    // Start is called before the first frame update
    void Start()
    {
        CarInFront = false;
        Speed = startSpeed;
        // ai = GetComponent<UnityEngine.AI.NavMeshAgent>();
        BackOfCar = this.gameObject.transform.GetChild(0).gameObject;
        // ai.speed = Speed;
        if(target == null)
            {
                target = PatrolPoints[0].transform;
                // UpdateDestination(target.position);
            }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = rotat.rotation;
        float singleStep = Speed * Time.deltaTime;

        wp = PatrolPoints[currentWaypoint];
        Vector2 targetDirection = wp.position - transform.position;
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            transform.position = wp.position;
            currentWaypoint = (currentWaypoint + 1) % PatrolPoints.Count;
        }
        else
        {
            //  Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            //Vector3 newPosition = new Vector3(moveHorizontal, 0.0f, moveVertical);
            //transform.LookAt(newPosition + transform.position);
            transform.position = Vector3.MoveTowards(transform.position, wp.position, Speed * Time.deltaTime);
            // transform.rotation = Quaternion.LookRotation(newDirection);
        }
        //TargetPos = new Vector3(transform.position.x , transform.position.y, wp.transform.position.z);

       // Quaternion.Euler direction = (wp.position.z - transform.position.z);
        //Quaternion rotation = Quaternion.Euler(0, 0, wp.position.z);
        //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        //transform.Rotate(TargetPos);


            // Vector3 targ = wp.transform.position;
            // targ.x = 0f;
            //  Vector3 objectPos = transform.position;
            // targ.z = targ.z - objectPos.z;
            // targ.y = targ.y - objectPos.y;
 
            // float angle = Mathf.Atan2(targ.y, targ.z) * Mathf.Rad2Deg;
            //     transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        if (Speed >= startSpeed)
        {
            Speed = startSpeed;
        }
        if (Speed <= 0)
        {
            Speed = 0;
        }
    }

    // public virtual void UpdateDestination(Vector3 newDestination)
    // {
    //     ai.destination = newDestination;
    // }
    // void Patrol()
    // {
    //         if (GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance < 0.5f && atDestination == false)
    //     {
    //         atDestination = true;
    //         //if the player isnt at the last point
    //         if (patrolNum < PatrolPoints.Count - 1)
    //         {
    //             patrolNum++;
    //         }
    //         //if the player is at the last point go to the first one
    //         else
    //         {
    //             patrolNum = 0;

    //         }
    //         target = PatrolPoints[patrolNum];
    //         UpdateDestination(target.position);
    //     }
    //     else
    //     {
    //         atDestination = false;
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Car")
        {
            wp = Reverse;
            Speed = startSpeed;
            Invoke(nameof(ResetAttack2), 5f);
        }
        if (other.gameObject.tag == "Player" && isStop == false)
        {
            isStop=true;
            Speed = 0;
            Invoke(nameof(ResetAttack), stopTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
         if (other.gameObject.tag == "BackOfCar" && other.gameObject.tag != "Car")
        {
            Speed = 0;
            // Speed = Speed - (Time.deltaTime * 2);
        }

            if (other.gameObject.tag == "Stop" && isStop == false)
        {
            isStop=true;
            Speed = 0;
            Invoke(nameof(ResetAttack), stopTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "BackOfCar")
        {
            Speed = startSpeed;
        }
        if (other.gameObject.tag == "Stop")
        {
            isStop = false;
        }
    }

    public virtual void ResetAttack()
    {
        Speed = startSpeed;
    }
    public virtual void ResetAttack2()
    {
       wp = PatrolPoints[currentWaypoint];
    }
}
