using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float sizeTimer = 0;
    private float sizeVal = 0;
    private Camera zoomCamera;
    [SerializeField] private float defaultSize = 5;

    // Start is called before the first frame update
    void Start()
    {
        zoomCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //if sizetimer (set by GrowSword function) is greater than 0, increase the size of the sword via linear interpolation
        if (sizeTimer > 0)
        {
            sizeTimer -= Time.deltaTime; //count down the timer
            zoomCamera.orthographicSize = Mathf.Lerp(zoomCamera.orthographicSize, sizeVal, Time.deltaTime * 2f);
        }
        //else return to the regular size using the same method
        else
        {
            zoomCamera.orthographicSize = Mathf.Lerp(zoomCamera.orthographicSize, defaultSize, Time.deltaTime * 2f);
        }
    }

    public void Zoom(float zoomLevel, float time)
    {
        sizeVal = zoomLevel;
        sizeTimer = time;
    }
}
