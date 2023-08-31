using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlLeftEnemy : MonoBehaviour
{
    public Transform target;
    public float sideSpeed;
    public float forwardSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //moves the enemy towards the centre
        //as long as side speed is faster than forawrd speed it will move in a cool spiral pattern
        transform.position += (transform.right * - 1) * sideSpeed * Time.deltaTime;
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;
        // makes the enemy look at the centre of the world and has them be the right way up its in upfate incase the sword moves it
        transform.LookAt(target, Vector3.back);
    }
}
