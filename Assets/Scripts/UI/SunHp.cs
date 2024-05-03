using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunHp : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateSunHp(int hp)
    {
        text.text = "Sun remaining HP: " + hp.ToString();
    }
}
