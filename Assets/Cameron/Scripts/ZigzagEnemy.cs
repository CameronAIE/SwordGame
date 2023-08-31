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

    // Start is called before the first frame update
    void Start()
    {
        timer = (Vector3.Distance(target.position, transform.position) / sideSpeed) * zag;
    }

    // Update is called once per frame
    void Update()
    {
        //moves the enemy towards the centre
        //float deltaSpeed = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, deltaSpeed);
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
