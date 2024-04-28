using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainGui : MonoBehaviour
{
    public GameObject towerStatsPanel;

    private TextMeshProUGUI[] towerStatsText;

    private void Start()
    {
        towerStatsText = towerStatsPanel.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void ToggleTowerStatsPanel(TowerLevel tower)
    {
        towerStatsPanel.SetActive(!towerStatsPanel.activeSelf);

        if (towerStatsPanel.activeSelf)
        {
            towerStatsText[0].text = "Level: " + tower.Level;
            towerStatsText[1].text = "Damage: " + tower.Damage;
            towerStatsText[2].text = "Range: " + tower.Range;
            towerStatsText[3].text = "Health: " + tower.Health;
            towerStatsText[4].text = "Bullet Speed: " + tower.BulletSpeed;
            towerStatsText[5].text = "Attack Speed: " + tower.AttackSpeed;
            towerStatsText[6].text = "Upgrade Price: " + tower.UpgradePrice;
        }
        
    }

    public void ToggleTowerStatsPanel()
    {
        towerStatsPanel.SetActive(!towerStatsPanel.activeSelf);
    }

    public void ToggleTowerStatsPanel(bool b)
    {
        towerStatsPanel.SetActive(b);
    }
}
