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
    [SerializeField] private GameObject Main, Slime;

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

    public void HelpMenu()
    {
        if (Slime.activeSelf)
        {
            Slime.SetActive(false);
            Main.SetActive(true);
        }
        else { Slime.SetActive(true);
            Main.SetActive(false);}
    }

    public void ExitGame() // shuts down game
    {
        Application.Quit();
    }
}
