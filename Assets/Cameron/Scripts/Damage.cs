using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    [SerializeField]
    private float health;
    [SerializeField]
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }

    void TakeDamage(float damage)
    {
        if (timer <= 0)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Centre")
        {
            SphereManager sm = other.gameObject.GetComponent<SphereManager>();
            sm.ObjDamage(damage);
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
