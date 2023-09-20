using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEnemy : MonoBehaviour
{

    public Transform target;
    public float speed;
    public float timer;
    public Material hittable;
    public Material unhittable;
    private float realTimer;
    private GameObject redColour;

    /// <summary>
    /// finds the object to turnn off and on and sets the real timer
    /// </summary>
    void Start()
    {
        //the actual game object doesnt have the materail to chaange the colour instead its one of its children so we just turn the red one off and on
        Identifier identifier = gameObject.GetComponentInChildren<Identifier>();
        redColour = identifier.gameObject;
        realTimer = timer;
    }

    /// <summary>
    /// will look at the center then move forwards but also uses a timer to change states between hittable and unhittable
    /// </summary>
    void Update()
    {
        // makes the enemy look at the centre of the world and has them be the right way up its in upfate incase the sword moves it
        transform.LookAt(target, Vector3.back);

        realTimer -= Time.deltaTime;
        //moves the enemy towards the centre
        float deltaSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, deltaSpeed);

        //when time runs out we turn the red off or on
        if (realTimer <= 0)
        {
            if(gameObject.tag == "Enemy")
            {
                redColour.SetActive(false);
                gameObject.tag = "Unhittable";
                realTimer = timer;

            } else
            {
                redColour.SetActive(true);
                gameObject.tag = "Enemy";
                realTimer = timer;
            }
        }
    }
}
