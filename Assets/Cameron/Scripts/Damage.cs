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
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindObjectOfType<SphereManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        realITime -= Time.deltaTime;
        //keeps it on the ground becuase the physics can be weird
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

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

    //this is just for taking damage from the lasers
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {
            Laser laserScript = other.gameObject.GetComponent<Laser>();
            TakeDamage(laserScript.damage);
            Destroy(laserScript.gameObject);
        }
    }

    //destroys it self and if true adds score
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
    public void PushBack(float force)
    {
        rb.AddForce((transform.forward * -1) * force, ForceMode.Impulse);
    }
}
