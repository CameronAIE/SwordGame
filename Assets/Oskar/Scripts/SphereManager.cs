using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    [SerializeField]
    private int Score, Health, Highscore, maxHealth;
    [SerializeField]
    private TextMeshProUGUI healthText, scoreText;
    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        Score = 0;
        healthText.text = $"Health\n{Health} | 100";
        Highscore = PlayerPrefs.GetInt("highscore");
        scoreText.text = $"Highscore:\n{Highscore}\nScore:\n{Score}";
    }
    public int getScore()
    { return Score; }
    public int getHighScore()
    {
        return Highscore;
    }
    /// <summary>
    /// decreases health by dmg
    /// </summary>
    /// <param name="dmg"></param>
    public void ObjDamage(int dmg)
    {
        Health -= dmg;
        if (Health < 0)
        {
            Health = 0;
        }
        healthText.text = $"Health\n{Health} | 100";
    }

    /// <summary>
    /// adds "score" to total score
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        Score += score;
        SaveScore();
        scoreText.text = $"Highscore:\n{Highscore}\nScore:\n{Score}";
    }

    public bool IsDead()
    {
        return (Health <= 0) ? true : false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        { 
            ObjDamage(other.gameObject.GetComponent<Damage>().damage); 
            other.gameObject.GetComponent<Damage>().Die(false);
        }
        if (other.CompareTag("Death")) 
        { 
            other.gameObject.GetComponent<Damage>().Die(true); 
        }
        
    }

    private void OnDestroy()
    {
        SaveScore();
    }
    public void SaveScore()
    {
        if(Score > Highscore)
        {
            PlayerPrefs.SetInt("highscore", Score);
            Highscore = PlayerPrefs.GetInt("highscore");
        }
    }
}
