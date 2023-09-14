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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //the laser just moves forward and destroys itself for the length of lifetime
        transform.position += transform.up * speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        
        Destroy(gameObject, lifeTime);
        
    }

}
