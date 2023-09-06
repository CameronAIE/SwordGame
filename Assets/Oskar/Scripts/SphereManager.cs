using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    [SerializeField]
    private int Score, Health;
    [SerializeField]
    private TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        Score = 0;
        healthText.text = $"Health\n{Health} | 100";
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
            AddScore(other.gameObject.GetComponent<Damage>().score);
            other.gameObject.GetComponent<Damage>().Die(true); 
        }
        
    }
}
