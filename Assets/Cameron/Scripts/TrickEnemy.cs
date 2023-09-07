using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickEnemy : MonoBehaviour
{
    public Transform target;
    public float speed;
    [SerializeField]
    private float timer;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // makes the enemy look at the centre of the world and has them be the right way up its in upfate incase the sword moves it
        transform.LookAt(target, Vector3.back);

        timer -= Time.deltaTime;
        //moves the enemy towards the centre
        float deltaSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, deltaSpeed);

        //turn into a new enemy when the time is over
        if (timer <= 0)
        {
            int choice = Random.Range(0, enemies.Length);
            Instantiate(enemies[choice], transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
