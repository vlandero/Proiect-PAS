using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsUi : MonoBehaviour
{
    private TextMeshProUGUI t;

    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoins(int coins)
    {
        t.text = "Coins: " + coins.ToString();
    }
}
