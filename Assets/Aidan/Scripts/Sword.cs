using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private HingeJoint hj;
    private Transform pivot;
    private MeshCollider col;
    [SerializeField] private SphereManager SphereManager;
    [SerializeField] private float smoothVal = 6f; //greater = faster smoothing
    [SerializeField] private float defaultSize = 1f; //The size the sword will rest to if no size modification is active
    [SerializeField] ParticleSystem sparks;
    [SerializeField] GameObject laser;

    public bool[] powerups; //0 = laser
                            //1 =...
    private float laserShoot = 0;

    private float sizeTimer = 0;
    private float sizeVal = 0;


    // Start is called before the first frame update
    void Start()
    {
        //gets required components
        hj = transform.GetComponentInParent<HingeJoint>();
        pivot = transform.parent;
        col = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //the position the sword is required to go
        Vector3 swordPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
  Input.mousePosition.y, Camera.main.nearClipPlane + 9));
        //updates the current position of the sword and smooths it with linear interpolation
        hj.connectedAnchor = Vector3.Lerp(hj.connectedAnchor, swordPos, Time.deltaTime * smoothVal);
        
        //if sizetimer (set by GrowSword function) is greater than 0, increase the size of the sword via linear interpolation
        if(sizeTimer > 0)
        {
            sizeTimer -= Time.deltaTime; //count down the timer
            float lerp = Mathf.Lerp(pivot.localScale.x, sizeVal, Time.deltaTime * 2f);
            pivot.localScale = new(lerp, 1,1);
        }
        //else return to the regular size using the same method
        else
        {
            float lerp = Mathf.Lerp(pivot.localScale.x, defaultSize, Time.deltaTime * 2f);
            pivot.localScale = new(lerp, 1, 1);
        }

        //enables spark particle effects if the sword is going sufficiently fast
        ParticleSystem.EmissionModule emission = sparks.emission;
        emission.enabled = Mathf.Abs(hj.velocity) > 500;

        Debug.Log(hj.angle);

        if (powerups[0])
        {
            if (laserShoot <= 0)
            {
                Transform tip = transform.GetChild(0);
                GameObject newLaser = Instantiate(laser, tip);

                newLaser.transform.parent = null;
                
                laserShoot = 0.15f;
            }
            else 
            {
                laserShoot -= Time.deltaTime;
            }
            //Instantiate(laser);
        }

        //Debug inputs for testing
        if (Input.GetMouseButton(0))
        {
            GrowSword(3f, 5f);
        }
        if (Input.GetMouseButton(1))
        {
            GrowSword(0.5f, 5f);
        }

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
            //Deals damage to base 

            //SphereManager.ObjDamage(collision.transform.GetComponent<Damage>().damage);
            Debug.Log("base has taken damage!");
        }
    }

    /// <summary>
    /// Grows the sword to a specified size for a set amount of time
    /// </summary>
    /// <param name="size">Size of the sword measured in scale.</param>
    /// <param name="time">Time of powerup measured in seconds.</param>
    public void GrowSword(float size, float time)
    {
        sizeVal = size;
        sizeTimer = time;
    }

}
