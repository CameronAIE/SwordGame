using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        //spawn random enemy from array
        int choice = Random.Range(0, enemies.Length);
        Instantiate(enemies[choice], transform.position, transform.rotation);
    }
}
