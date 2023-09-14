using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTimer : MonoBehaviour
{
    private TextMeshProUGUI powerText;
    private Slider countDown;

    // Start is called before the first frame update
    void Awake()
    {
        powerText = GetComponent<TextMeshProUGUI>();
        countDown = transform.GetChild(0).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //counts down untill value is 0 in which it deletes itself
        countDown.value -= Time.deltaTime;
        if (countDown.value <= 0) Destroy(gameObject);
    }

    public void Initialize(string powerUpName, float time)
    {
        //sets text for object
        powerText.text = powerUpName;
        //sets maxValue and value to starting time
        countDown.maxValue = time;
        countDown.value = time;
    }
}
