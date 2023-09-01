using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private HingeJoint hj;
    private Rigidbody rb;
    private MeshCollider col;
    [SerializeField] private SphereManager SphereManager;


    // Start is called before the first frame update
    void Start()
    {
        //gets required components
        hj = transform.GetComponentInParent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //the position the sword is required to go
        Vector3 swordPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
  Input.mousePosition.y, Camera.main.nearClipPlane + 9));
        //updates the current position of the sword
        hj.connectedAnchor = Vector3.Lerp(hj.connectedAnchor, swordPos, Time.deltaTime * 4f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Debug.Log("Damage dealt: " + Mathf.Abs(hj.velocity));
            

            //gets the component of the collided object and then deals damage based on angular velocity
            collision.transform.GetComponent<Damage>().TakeDamage(Mathf.Abs(hj.velocity));

        }
        else if (collision.transform.CompareTag("Death"))
        {
            //SphereManager.ObjDamage(collision.transform.GetComponent<Damage>().damage);

            //Deals damage to base 

            Debug.Log("base has taken damage!");
        }
    }


}
