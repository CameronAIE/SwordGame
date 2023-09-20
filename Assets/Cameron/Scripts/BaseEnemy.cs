using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public Transform target;
    public float speed;

    /// <summary>
    /// the update function for this enemy will just have it look at the center and move forwards
    /// </summary>
    void Update()
    {
        //moves the enemy towards the centre
        float deltaSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, deltaSpeed);

        // makes the enemy look at the centre of the world and has them be the right way up its in upfate incase the sword moves it
        transform.LookAt(target, Vector3.back);
    }
}
