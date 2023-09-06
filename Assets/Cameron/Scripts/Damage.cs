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
    [SerializeField]
    private int score;
    [SerializeField]
    private GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindObjectOfType<SphereManager>();
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

    private void OnTriggerEnter(Collider other)
    {
        //deals damage to the centre
        if (other.gameObject.tag == "Centre")
        {
            sm = other.gameObject.GetComponent<SphereManager>();
            sm.ObjDamage(damage);
            Die(false);
        }
    }

    void Die(bool hit)
    {
        if (!hit)
        {
            sm.AddScore(score);
        }
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
