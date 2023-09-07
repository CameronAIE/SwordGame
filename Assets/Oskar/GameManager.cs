using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject DebugMenu, PauseMenu;
    [SerializeField]
    private SphereManager SphereManager;
    private bool DebugMode;
    private float spawnTimer; //countdown value for spawning
    [SerializeField] private float difficulty; //maximum wait time between enemies
    [SerializeField] private Spawn[] spawnLocations; //locations of all of the spawn points
    [SerializeField] private int maxEnemies = 15; //maximum allowed enemies currently active in scene
    [SerializeField] List<GameObject> enemiesList; //list of all active enemies
     

    public enum GameState
    {
        Active,
        Paused,
        Killed
    }
    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        DebugMode = false;
        gameState = GameState.Active;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Active:
                
                if (spawnTimer > difficulty && enemiesList.Count <= maxEnemies)
                {
                    spawnTimer = 0;
                    enemiesList.Add(spawnLocations[Random.Range(0, spawnLocations.Length)].SpawnEnemy());
                    difficulty -= 0.005f;
                    Mathf.Clamp(spawnTimer, 0.2f, 100);
                }

                spawnTimer += Time.deltaTime;

                foreach(GameObject enemy in enemiesList)
                {
                    if (!enemy)
                    {
                        enemiesList.Remove(enemy);
                        break;
                    }
                }

                break;
            case GameState.Paused:
                
                break;
            case GameState.Killed:

                break;
        }

        

    }

    public void PauseGame()
    {
        switch (gameState)
        {
            case GameState.Paused:
                Time.timeScale = 1;
                gameState = GameState.Active;
                break;
            case GameState.Active:
                Time.timeScale = 0;
                gameState = GameState.Paused;
                break;
            default: break;
        }
    }

    public void ToggleDebug()
    {
        DebugMode = !DebugMode;
        DebugMenu.SetActive(DebugMode);
    }
}
