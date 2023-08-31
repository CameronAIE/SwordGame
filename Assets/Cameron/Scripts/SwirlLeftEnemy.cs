using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlLeftEnemy : MonoBehaviour
{
    public Transform target;
    public float sideSpeed;
    public float forwardSpeed;
    public float health;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //moves the enemy towards the centre
        //float deltaSpeed = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, deltaSpeed);
        transform.position += (transform.right * - 1) * sideSpeed * Time.deltaTime;
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;
        // makes the enemy look at the centre of the world and has them be the right way up its in upfate incase the sword moves it
        transform.LookAt(target, Vector3.back);
    }
}
