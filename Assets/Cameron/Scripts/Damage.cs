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
    private float ITime;
    private SphereManager sm;
    [SerializeField]
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindObjectOfType<SphereManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ITime -= Time.deltaTime;
    }

    void TakeDamage(float damage)
    {
        if (ITime <= 0)
        {
            health -= damage;
            if (health <= 0)
            {
                Die(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
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
        Destroy(gameObject);
    }
}
