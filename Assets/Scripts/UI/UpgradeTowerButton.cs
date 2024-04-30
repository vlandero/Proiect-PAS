using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeTowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Planet planet;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(planet.tower.level < TowerData.TowerTypes[planet.tower.towerName].Levels.Count)
        {
            var currentLevel = TowerData.TowerTypes[planet.tower.towerName].Levels[planet.tower.level - 1];
            var nextLevel = TowerData.TowerTypes[planet.tower.towerName].Levels[planet.tower.level];
            CanvasManager.instance.mainGui.SetTowerStats(currentLevel, nextLevel);
        }
        else
        {
            CanvasManager.instance.mainGui.SetTowerStats(TowerData.TowerTypes[planet.tower.towerName].Levels[planet.tower.level - 1]);
        }
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(false);
    }
}
