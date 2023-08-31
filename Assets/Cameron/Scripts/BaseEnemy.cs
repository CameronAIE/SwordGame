using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public Transform target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target, Vector3.back);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * speed;
    }
}
