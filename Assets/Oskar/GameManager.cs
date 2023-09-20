using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject DebugMenu, PauseMenu, DeathMenu, pauseButton, pauseTxt;
    [SerializeField] private SphereManager sphereManager;
    [SerializeField] private TextMeshProUGUI endScore;
    private bool DebugMode;
    [SerializeField] private Sword player;
    private float spawnTimer; //countdown value for spawning
    [SerializeField] private float difficulty; //maximum wait time between enemies
    [SerializeField] private Spawn[] spawnLocations; //locations of all of the spawn points
    [SerializeField] private int maxEnemies = 15; //maximum allowed enemies currently active in scene
    [SerializeField] List<GameObject> enemiesList; //list of all active enemies
    [Header("PowerUps")]
    [SerializeField] private PowerUpList powerUpList; //this script should be attatched to a gameobject that has a vertical layout group
    [SerializeField] private float[] powerUpTimers;
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
        Time.timeScale = 1;
        DebugMode = false;
        PauseMenu.SetActive(false);
        gameState = GameState.Active;


#if UNITY_STANDALONE || UNITY_WEBGL
    pauseButton.SetActive(false);
    Cursor.lockState = CursorLockMode.Locked;
#endif
#if UNITY_ANDROID
        pauseTxt.SetActive(false);
        
#endif
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Active:
                
                if (spawnTimer > difficulty && enemiesList.Count < maxEnemies)
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
                //subtracts time from the current powerups and disables them if they have expired
                for(int i = 0; i < powerUpTimers.Length; i++)
                {
                    if (powerUpTimers[i] > 0)
                    {
                        powerUpTimers[i] -= Time.deltaTime;
                    }
                    else
                    {
                        //only the first three powerups need manual removal
                        if (i < 3)
                        {
                            player.DisablePowerUp(i);
                        }
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
#if UNITY_STANDALONE || UNITY_WEBGL
                Cursor.lockState = CursorLockMode.Locked;
#endif
                PauseMenu.SetActive(false);
                break;
            case GameState.Active:
                Time.timeScale = 0;
                gameState = GameState.Paused;
                Cursor.lockState = CursorLockMode.None;
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
        player.gameObject.SetActive(false);
        sphereManager.SaveScore();
        DeathMenu.SetActive(true);
        foreach(GameObject enemy in enemiesList)
        {
            enemy.SetActive(false);
        }
        endScore.text = $"Score: {sphereManager.getScore()} | Highscore: {sphereManager.getHighScore()}";
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void PowerUp()
    {
        if (Random.Range(0, chance) == 0)
        {
            int result = Random.Range(1, 10);

            switch (result)
            {
                case 1:
                    //laser
                    player.EnablePowerUp(0);
                    powerUpList.CreateText("Laser!", 20);
                    powerUpTimers[0] = 20;
                    break;
                case 2:
                    //light sword
                    player.EnablePowerUp(1);
                    powerUpList.CreateText("Light Sword", 20);
                    powerUpTimers[1] = 20;
                    break;
                case 3:
                    //auto swing
                    player.EnablePowerUp(2);
                    powerUpList.CreateText("Auto Swing", 20);
                    powerUpTimers[2] = 20;
                    break;
                case 4:
                    //grow sword
                    powerUpList.CreateText("Big Sword", bigSwordTime);
                    player.GrowSword(swordSize, bigSwordTime);
                    powerUpTimers[3] = bigSwordTime;
                    break;
                case 5:
                    //zooms out camera
                    powerUpList.CreateText("Zoom out!", zoomTime);
                    cameraZoom.Zoom(zoom, zoomTime);
                    powerUpTimers[4] = zoomTime;
                    break;
                case 6:
                    //damages player
                    powerUpList.CreateText("Ouch! Damage!", 3);
                    sphereManager.ObjDamage(powerUpDamage);
                    //powerUpTimers[3] = bigSwordTime;
                    break;
                case 7:
                    //heals player
                    powerUpList.CreateText("Nice! Healing!", 3);
                    sphereManager.ObjDamage(powerUpHeal);
                    break;
                case 8:
                    //pushes all enemies away
                    powerUpList.CreateText("Enemy PushBack!", 3);
                    foreach (GameObject enemy in enemiesList)
                    {
                        enemy.GetComponent<Damage>().PushBack(pushBack);
                    }
                    break;
                case 9:
                    //kills all enemies
                    powerUpList.CreateText("Nuke!", 3);
                    foreach (GameObject enemy in enemiesList)
                    {
                        enemy.GetComponent<Damage>().PowerUpDie();
                    }
                    break;
            }
        }
        
    }
}
