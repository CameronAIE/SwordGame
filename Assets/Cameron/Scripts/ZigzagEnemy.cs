using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagEnemy : MonoBehaviour
{
    public Transform target;
    public float sideSpeed;
    public float forwardSpeed;
    private bool right;
    private float timer;
    public float zag;

    /// <summary>
    /// sets the timer based off of distance from the center to keep the zigs and zags equal
    /// </summary>
    void Start()
    {
        timer = (Vector3.Distance(target.position, transform.position) / sideSpeed) * zag;
    }

    /// <summary>
    /// looks at the center moves forwards and uses the timer to change sideways directions and sets that timer based off of the distance to the center
    /// </summary>
    void Update()
    {
        //moves the enemy towards the centre
        

        //i used the swirl code but made a timer change the direction based off of the distance from the centre
        timer -= Time.deltaTime;
        if (right)
        {
            transform.position += transform.right * sideSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += (transform.right * -1) * sideSpeed * Time.deltaTime;
        }
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        if (timer <= 0)
        {
            if(right)
            {
                right = false;
            }
            else
            {
                right = true;
            }
            timer = (Vector3.Distance(target.position, transform.position) / sideSpeed) * zag;
        }

        // makes the enemy look at the centre of the world and has them be the right way up its in upfate incase the sword moves it
        transform.LookAt(target, Vector3.back);
    }
}
