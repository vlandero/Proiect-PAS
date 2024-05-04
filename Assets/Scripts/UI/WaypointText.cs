using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum WaypointTextType
{
    Enemy,
    Time
}

public class WaypointText : MonoBehaviour
{
    private float timer = 30;

    private TextMeshProUGUI text;
    [SerializeField] private WaypointTextType type;

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
        if(type == WaypointTextType.Time)
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
}
