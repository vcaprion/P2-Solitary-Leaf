using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deliveries : MonoBehaviour
{
    public int randomNumber;
    private int lastNumber;
    // Start is called before the first frame update
    void Start()
    {
        NewRandomNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if (randomNumber = 1)
        {
            
        }
    }
       public virtual void NewRandomNumber()
{
    randomNumber = Random.Range(1, 3);
    if (randomNumber == lastNumber)
    {
        randomNumber = Random.Range(1, 3);
    }
    lastNumber = randomNumber;
}
}
