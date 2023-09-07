using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemies;
    [SerializeField]
    private float timer;
    private float realTimer;
    private int avalibileEnemys = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //adding harder enemys over time
        realTimer += Time.deltaTime;
        if (realTimer >= timer)
        {
            realTimer = Time.deltaTime;
            avalibileEnemys += 2;
            if (avalibileEnemys > enemies.Length)
            {
                avalibileEnemys = enemies.Length;
            }
        }
    }

    public GameObject SpawnEnemy()
    {
        //spawn random enemy from array
        int choice = Random.Range(0, avalibileEnemys);
        return Instantiate(enemies[choice], transform.position, transform.rotation);
    }

    public GameObject SpawnSpecificEnenmy(int enemy)
    {
        // spawn specific enemy from array place and make sure the number is useable
        int realEnemy = enemy % enemies.Length;
        return Instantiate(enemies[realEnemy], transform.position, transform.rotation);
    }
}
