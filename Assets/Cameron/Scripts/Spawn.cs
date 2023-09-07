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

    public void SpawnEnemy()
    {
        //spawn random enemy from array
        int choice = Random.Range(0, avalibileEnemys);
        Instantiate(enemies[choice], transform.position, transform.rotation);
    }

    public void SpawnSpecificEnenmy(int enemy)
    {
        int realEnemy = enemy % enemies.Length;
        Instantiate(enemies[realEnemy], transform.position, transform.rotation);
    }
}
