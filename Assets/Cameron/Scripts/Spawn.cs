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
    
    /// <summary>
    /// uses a timer to add to the amount of useable enemys over time
    /// </summary>
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

    /// <summary>
    /// will spawn a random enemy at its position rfom the avalible enemys
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnEnemy()
    {
        //spawn random enemy from array
        int choice = Random.Range(0, avalibileEnemys);
        return Instantiate(enemies[choice], transform.position, transform.rotation);
    }

    /// <summary>
    /// will spawn a specific enemy based off of an int
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public GameObject SpawnSpecificEnenmy(int enemy)
    {
        // spawn specific enemy from array place and make sure the number is useable
        int realEnemy = enemy % enemies.Length;
        return Instantiate(enemies[realEnemy], transform.position, transform.rotation);
    }
}
