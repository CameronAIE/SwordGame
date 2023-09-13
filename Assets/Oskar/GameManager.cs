using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject DebugMenu, PauseMenu;
    [SerializeField]
    private SphereManager sphereManager;
    private bool DebugMode;
    [SerializeField] private Sword player;
    private float spawnTimer; //countdown value for spawning
    [SerializeField] private float difficulty; //maximum wait time between enemies
    [SerializeField] private Spawn[] spawnLocations; //locations of all of the spawn points
    [SerializeField] private int maxEnemies = 15; //maximum allowed enemies currently active in scene
    [SerializeField] List<GameObject> enemiesList; //list of all active enemies
    [Header("PowerUps")]
    //[SerializeField] private
    [SerializeField] private int chance; // chance of a power up 0 is one in one 100 is one in 101
    [SerializeField] private int powerUpDamage;
    [SerializeField] private float pushBack;
    [SerializeField] private float swordSize;
    [SerializeField] private float bigSwordTime;
    [SerializeField] private int powerUpHeal;
    [SerializeField] private CameraZoom cameraZoom;
    [SerializeField] private float zoom;
    [SerializeField] private float zoomTime;
     

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
        PauseMenu.SetActive(false);
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
                    difficulty -= 0.001f;
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
                if (sphereManager.IsDead())
                {
                    gameState = GameState.Killed;
                }
                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGame();
                }
                break;
            case GameState.Paused:
                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGame();
                }
                break;
            case GameState.Killed:
                Dying();
                break;
        }
    }

    public void QuitToMenu()
    {
        sphereManager.SaveScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        switch (gameState)
        {
            case GameState.Paused:
                Time.timeScale = 1;
                gameState = GameState.Active;
                PauseMenu.SetActive(false);
                break;
            case GameState.Active:
                Time.timeScale = 0;
                gameState = GameState.Paused;
                PauseMenu.SetActive(true);
                break;
            default: break;
        }
    }

    public void ToggleDebug()
    {
        DebugMode = !DebugMode;
        DebugMenu.SetActive(DebugMode);
    }

    private void Dying()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PowerUp()
    {
        if (Random.Range(0, chance) == 0)
        {
            int result = Random.Range(1, 10);

            switch (result)
            {
                case 1:

                    player.EnablePowerUp(1);
                    break;
                case 2:
                    player.EnablePowerUp(0); ;
                    break;
                case 3:
                    foreach (GameObject enemy in enemiesList)
                    {
                        enemy.GetComponent<Damage>().Die(true);
                    }
                    break;
                case 4:
                    sphereManager.ObjDamage(powerUpDamage);
                    break;
                case 5:
                    foreach (GameObject enemy in enemiesList)
                    {
                        enemy.GetComponent<Damage>().PushBack(pushBack);
                    }
                    break;
                case 6:
                    player.GrowSword(swordSize, bigSwordTime);
                    break;
                case 7:
                    sphereManager.ObjDamage(powerUpHeal);
                    break;
                case 8:
                    cameraZoom.Zoom(zoom, zoomTime);
                    break;
                case 9:
                    player.EnablePowerUp(3);
                    break;
            }
        }
        
    }
}
