using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private HingeJoint hj;
    private Rigidbody rb;
    private Transform pivot;
    [Header("Values")]
    public bool inputType; //true = virtual cursor enabled 
    public bool[] powerups; //0 = laser
                            //1 = lighter sword
                            //2 = autospin
    [SerializeField] private float smoothVal = 6f; //greater = faster smoothing
    [SerializeField] private float defaultSize = 1f; //The size the sword will rest to if no size modification is active
    [SerializeField] private bool debugMode; //enables debug inputs 
    [Header("Prefabs")]
    [SerializeField] GameObject laser;
    [SerializeField] private GameObject cursorPrefab;
    [Header("Assigned objects")]
    [SerializeField] private SphereManager SphereManager; //prehaps I could compress this?
    [SerializeField] ParticleSystem sparks;
    [SerializeField] ParticleSystem laserFire;
    [SerializeField] ParticleSystem lightEffect;
    [SerializeField] GameObject slashEffect;
    [SerializeField] private float dampner;
    [SerializeField] Camera camera;

    private float laserShoot = 0;

    private float sizeTimer = 0;
    private float sizeVal = 0;

    private GameObject cursorObject;


    // Start is called before the first frame update
    void Start()
    {
        //gets required components
        rb = GetComponent<Rigidbody>();
        hj = transform.GetComponentInParent<HingeJoint>();
        pivot = transform.parent;
    #if UNITY_STANDALONE || UNITY_WEBGL
        cursorObject = Instantiate(cursorPrefab);
    #endif
    }

    private void FixedUpdate()
    {
    #if UNITY_ANDROID
        //the position the sword is required to go
        Vector3 swordPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, Camera.main.nearClipPlane + 9));
        //updates the current position of the sword and smooths it with linear interpolation
        hj.connectedAnchor = Vector3.Lerp(hj.connectedAnchor, swordPos, Time.deltaTime * smoothVal);
    #endif
    #if UNITY_STANDALONE || UNITY_WEBGL
        hj.connectedAnchor = Vector3.Lerp(hj.connectedAnchor, cursorObject.transform.position, Time.deltaTime * smoothVal);
    #endif
    }

    // Update is called once per frame
    void Update()
    {
    #if UNITY_STANDALONE || UNITY_WEBGL
        Vector2 input = new((Input.GetAxisRaw("Mouse X") / dampner) * Time.timeScale, (Input.GetAxisRaw("Mouse Y") / dampner) * Time.timeScale);

        cursorObject.transform.position = new(Mathf.Clamp(cursorObject.transform.position.x + input.x, -9f * (camera.orthographicSize / 5f), 9f * (camera.orthographicSize / 5f)),
            Mathf.Clamp(cursorObject.transform.position.y + input.y, -5f * (camera.orthographicSize / 5f), 5f * (camera.orthographicSize / 5f)), -0.5f);
#endif



        //if sizetimer (set by GrowSword function) is greater than 0, increase the size of the sword via linear interpolation
        if (sizeTimer > 0)
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

        //Debug.Log(hj.angle);

        //if laser powerup is active
        if (powerups[0])
        {
            //if timer has reached 0
            if (laserShoot <= 0)
            {
                //creates laser and parents it (so the laser is centered properly)
                Transform tip = transform.GetChild(0);
                GameObject newLaser = Instantiate(laser, tip);
                //unassigns the parent
                newLaser.transform.parent = null;
                //sets a timer for the next shot
                laserShoot = 0.1f;
            }
            else 
            {
                //subtracts timer
                laserShoot -= Time.deltaTime;
            }
        }

        //Time.timeScale = Mathf.Lerp(Time.timeScale, 1, Time.deltaTime * 5f);

        //Debug inputs for testing
        if (debugMode)
        {
            if (Input.GetMouseButton(0))
            {
                GrowSword(3f, 5f);
            }
            if (Input.GetMouseButton(1))
            {
                GrowSword(0.5f, 5f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!powerups[0]) EnablePowerUp(0);
                else DisablePowerUp(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (!powerups[1]) EnablePowerUp(1);
                else DisablePowerUp(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (!powerups[2]) EnablePowerUp(2);
                else DisablePowerUp(2);
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.CompareTag("Enemy"))
        {
            //Time.timeScale = 0.01f;
            Debug.Log("Damage dealt: " + Mathf.Abs(hj.velocity));

            GameObject slash = Instantiate(slashEffect, new(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z - 1f), Quaternion.identity);
            Destroy(slash, 0.25f);
            //gets the component of the collided object and then deals damage based on angular velocity
            collision.transform.GetComponent<Damage>().TakeDamage(Mathf.Abs(hj.velocity));

        }
        else if (collision.transform.CompareTag("Death"))
        {
            //Deals damage to base if death enemy is hit

            SphereManager.ObjDamage(2);
            collision.transform.GetComponent<Damage>().Die(false);
            //Debug.Log("base has taken damage!");
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

    /// <summary>
    /// Enables a powerup based on the index provided. List of powerups is next to the powerups[] bool;
    /// </summary>
    public void EnablePowerUp(int index)
    {
        switch (index)
        {
            case 0:
                ParticleSystem.EmissionModule fireEmission = laserFire.emission;
                fireEmission.enabled = true;
                powerups[0] = true;
                break;
            case 1:
                ParticleSystem.EmissionModule lightEmission = lightEffect.emission;
                lightEmission.enabled = true;
                rb.mass = 400;
                rb.drag = 0;
                smoothVal = 10;
                powerups[1] = true;
                break;
            case 2:
                powerups[2] = true;
                hj.useMotor = true;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Disables a powerup based on the index provided. List of powerups is next to the powerups[] bool;
    /// </summary>
    public void DisablePowerUp(int index)
    {
        switch (index)
        {
            case 0:
                ParticleSystem.EmissionModule fireEmission = laserFire.emission;
                fireEmission.enabled = false;
                powerups[0] = false;
                break;
            case 1:
                ParticleSystem.EmissionModule lightEmission = lightEffect.emission;
                lightEmission.enabled = false;
                rb.mass = 100;
                rb.drag = 2;
                smoothVal = 8;
                powerups[1] = false;
                break;
            case 2:
                powerups[2] = false;
                hj.useMotor = false;
                break ;
            default:
                break;
        }
    }

}
