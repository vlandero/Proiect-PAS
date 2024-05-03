using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaypointText : MonoBehaviour
{
    private float timer = 30;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetTimer(float time)
    {
        timer = time;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            text.text = "0";
        }
        else
        {
            text.text = timer.ToString("F0");
        }
    }
}
