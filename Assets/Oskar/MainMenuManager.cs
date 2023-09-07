using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    private int highScore;
    [SerializeField]
    private GameObject highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore");
        highScoreText.GetComponent<TextMeshProUGUI>().text = $"Highscore: \n {highScore}";
    }

    public void PlayGame() // goes into main scene
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame() // shuts down game
    {
        Application.Quit();
    }
}
