using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    [SerializeField]
    private int Score, Health, Highscore;
    [SerializeField]
    private TextMeshProUGUI healthText, scoreText;
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        Score = 0;
        healthText.text = $"Health\n{Health} | 100";
        Highscore = PlayerPrefs.GetInt("highscore");
        scoreText.text = $"Highscore:\n{Highscore}\nScore:\n{Score}";
    }

    /// <summary>
    /// decreases health by dmg
    /// </summary>
    /// <param name="dmg"></param>
    public void ObjDamage(int dmg)
    {
        Health -= dmg;
        healthText.text = $"Health\n{Health} | 100";
    }

    /// <summary>
    /// adds "score" to total score
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        Score += score;
        if (Score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", Score);
        }
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
        if (Score > Highscore)
        {
            PlayerPrefs.SetInt("highscore", Score);
        }
    }
}
