using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlRightEnemy : MonoBehaviour
{
    public Transform target;
    public float sideSpeed;
    public float forwardSpeed;

    /// <summary>
    /// same as the base enemy it looks at the center and moves forward but it also moves sideways based off of a second speed
    /// </summary>
    void Update()
    {
        //moves the enemy towards the centre
        //as long as side speed is faster than forawrd speed it will move in a cool spiral pattern
        transform.position += transform.right * sideSpeed * Time.deltaTime;
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;
        // makes the enemy look at the centre of the world and has them be the right way up its in upfate incase the sword moves it
        transform.LookAt(target, Vector3.back);
    }
}
