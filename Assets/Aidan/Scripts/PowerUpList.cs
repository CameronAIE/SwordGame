using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpList : MonoBehaviour
{
    [SerializeField] private GameObject powerBase;
    //creates a new List timer object that counts down until it has expired 
    public void CreateText(string text, float time)
    {
        GameObject newList = Instantiate(powerBase, transform);
        PowerUpTimer timer = newList.GetComponent<PowerUpTimer>();
        timer.Initialize(text, time);
    }
}
