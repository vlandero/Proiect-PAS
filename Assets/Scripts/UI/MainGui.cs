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

    public void SetTowerStats(TowerLevel tower)
    {
        towerStatsText[0].text = "Level: " + tower.Level;
        towerStatsText[1].text = "Damage: " + tower.Damage;
        towerStatsText[2].text = "Range: " + tower.Range;
        towerStatsText[3].text = "Health: " + tower.Health;
        towerStatsText[4].text = "Bullet Speed: " + tower.BulletSpeed;
        towerStatsText[5].text = "Attack Speed: " + tower.AttackSpeed;
        towerStatsText[6].text = "Upgrade Price: " + tower.UpgradePrice;
    }

    public void SetTowerStats(TowerLevel prevLevel, TowerLevel nextLevel)
    {
        towerStatsText[0].text = "Level: " + prevLevel.Level + " -> " + nextLevel.Level;
        towerStatsText[1].text = "Damage: " + prevLevel.Damage + " -> " + nextLevel.Damage + " (" + (nextLevel.Damage - prevLevel.Damage) + ")";
        towerStatsText[2].text = "Range: " + prevLevel.Range + " -> " + nextLevel.Range + " (" + (nextLevel.Range - prevLevel.Range) + ")";
        towerStatsText[3].text = "Health: " + prevLevel.Health + " -> " + nextLevel.Health + " (" + (nextLevel.Health - prevLevel.Health) + ")";
        towerStatsText[4].text = "Bullet Speed: " + prevLevel.BulletSpeed + " -> " + nextLevel.BulletSpeed + " (" + (nextLevel.BulletSpeed - prevLevel.BulletSpeed) + ")";
        towerStatsText[5].text = "Attack Speed: " + prevLevel.AttackSpeed + " -> " + nextLevel.AttackSpeed + " (" + (nextLevel.AttackSpeed - prevLevel.AttackSpeed) + ")";
        towerStatsText[6].text = "Upgrade Price: " + prevLevel.UpgradePrice + " -> " + nextLevel.UpgradePrice + " (" + (nextLevel.UpgradePrice - prevLevel.UpgradePrice) + ")";
    }

    public void ToggleTowerUpgradePanel()
    {

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