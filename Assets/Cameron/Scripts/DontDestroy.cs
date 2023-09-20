using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    /// <summary>
    /// stops this game object from being destroyed when there is a new scene
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// destroys any other of the same object
    /// </summary>
    private void Awake()
    {
        DontDestroy[] others = FindObjectsOfType(typeof(DontDestroy)) as DontDestroy[];
        foreach(DontDestroy b in others)
        {
            if (b != this)
            {
                Destroy(b.gameObject);
            }
        }
    }

}
