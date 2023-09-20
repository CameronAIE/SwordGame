using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Damage : MonoBehaviour
{
    public int damage;
    [SerializeField]
    private float health;
    [SerializeField]
    private float iTime;
    private float realITime;
    private SphereManager sm;
    private GameManager gm;
    public int score;
    [SerializeField]
    private GameObject particles;
    private Rigidbody rb;
    
    /// <summary>
    /// some components are found and set
    /// </summary>
    void Start()
    {
        sm = GameObject.FindObjectOfType<SphereManager>();
        gm = GameObject.FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// decreases a timer and keeps the enemy o9n the ground
    /// </summary>
    void Update()
    {
        realITime -= Time.deltaTime;
        //keeps it on the ground becuase the physics can be weird
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    /// <summary>
    /// this is called when when the enemy needs to take damage from the sword or the laser or other 
    /// </summary>
    public void TakeDamage(float damage)
    {
        //the itime is so the sword can keep calling if they keep colliding 
        if (realITime <= 0)
        {
            health -= damage;
            realITime = iTime;
            if (health <= 0)
            {
                Die(true);
            }
        }
    }

    /// <summary>
    /// the only trigger the enemy can collide with is the laser so this is just for taking damge from the laser
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {
            Laser laserScript = other.gameObject.GetComponent<Laser>();
            TakeDamage(laserScript.damage);
            Destroy(laserScript.gameObject);
        }
    }

    /// <summary>
    /// adds score depending on if it was killed by the player and then destroys it self and puts an explosion in its place
    /// </summary>
    /// <param name="addScore"></param>
    public void Die(bool addScore)
    {
        if (addScore)
        {
            sm.AddScore(score);
            gm.PowerUp();
        }
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    /// <summary>
    /// this function is specifcly for whem a nuke kills all the enemys at once so you dot get any extra power ups
    /// </summary>
    public void PowerUpDie()
    {
        sm.AddScore(score);
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    /// <summary>
    /// adds force backward to the enemy
    /// </summary>
    /// <param name="force"></param>
    public void PushBack(float force)
    {
        rb.AddForce((transform.forward * -1) * force, ForceMode.Impulse);
    }
}
