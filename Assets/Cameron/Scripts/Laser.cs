using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lifeTime;
    public float damage;

    /// <summary>
    /// this update moves the laser and destroys it when the timer is up
    /// </summary>
    void Update()
    {
        //the laser just moves forward and destroys itself for the length of lifetime
        transform.position += transform.up * speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        
        Destroy(gameObject, lifeTime);
        
    }

}
