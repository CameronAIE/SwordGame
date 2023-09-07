using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public Transform target;
    public float speed;
    private bool right;
    private bool left;
    private bool forward;
    private bool backward;
    [SerializeField]
    private float timerMin;
    [SerializeField]
    private float timerMax;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(timerMin, timerMax);

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //reset the directions of the enemy
        if (timer <= 0)
        {
            RandomDirections();
            timer = Random.Range(timerMin, timerMax);
        }
        
        //move acording to directions this is also based off of the swirl movement
        if (right)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if (left)
        {
            transform.position += (transform.right * -1) * speed * Time.deltaTime;
        }
        if (forward)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        } 
        else if (backward)
        {
            transform.position += (transform.forward * -1) * speed * Time.deltaTime;
        }

        // makes the enemy look at the centre of the world and has them be the right way up its in upfate incase the sword moves it
        transform.LookAt(target, Vector3.back);
    }

    void RandomDirections()
    {
        // i tried to give each direction an even chance to happen
        right = false;
        left = false;
        backward = false;
        forward = false;

        float lRDecider = Random.value;
        if (lRDecider <= 0.3333333333333333333333333333333f)
        {
            right = true;
        }
        else if (lRDecider >= .666666666666666666666666666666f)
        {
            left = true;
        }

        float fBDecider = Random.value;
        if (fBDecider <= .33333333333333333333333f)
        {
            forward = true;
        } else if (fBDecider >= .66666666666666666666666666f)
        {
            backward = true;
        }
    }
}
