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

                
                break;
            case GameState.Paused:
                
                break;
            case GameState.Killed:

                break;
        }

        if(spawnTimer > difficulty)
        {
            spawnTimer = 0;
            spawnLocations[Random.Range(0, spawnLocations.Length)].SpawnEnemy();
            difficulty -= 0.01f;
        }

        spawnTimer += Time.deltaTime;

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
