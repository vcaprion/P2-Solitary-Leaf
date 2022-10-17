using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    public List<Transform> PatrolPoints = new List<Transform>();
    private Transform target;
    public UnityEngine.AI.NavMeshAgent ai;
    private bool atDestination;
    public int patrolNum;
    public float Speed;
    private GameObject BackOfCar;
    private bool isStop;
    public float stopTime;


    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<UnityEngine.AI.NavMeshAgent>();
        BackOfCar = this.gameObject.transform.GetChild(0).gameObject;
        ai.speed = Speed;
        if(target == null)
            {
                target = PatrolPoints[0].transform;
                UpdateDestination(target.position);
            }
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    public virtual void UpdateDestination(Vector3 newDestination)
    {
        ai.destination = newDestination;
    }
    void Patrol()
    {
            if (GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance < 0.5f && atDestination == false)
        {
            atDestination = true;
            //if the player isnt at the last point
            if (patrolNum < PatrolPoints.Count - 1)
            {
                patrolNum++;
            }
            //if the player is at the last point go to the first one
            else
            {
                patrolNum = 0;

            }
            target = PatrolPoints[patrolNum];
            UpdateDestination(target.position);
        }
        else
        {
            atDestination = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
         if (other.gameObject.tag == "BackOfCar" && other.gameObject != BackOfCar)
        {
            ai.speed = 0;
        }
            if (other.gameObject.tag == "Stop" && isStop == false)
        {
            isStop=true;
            ai.speed = 0;
            Invoke(nameof(ResetAttack), stopTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BackOfCar")
        {
            ai.speed = Speed;
        }
        if (other.gameObject.tag == "Stop")
        {
            isStop = false;
        }
    }

    public virtual void ResetAttack()
    {
        ai.speed = Speed;
    }
}
