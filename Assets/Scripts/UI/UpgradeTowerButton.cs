using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeTowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Planet planet;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(planet.tower.level < PrefabManager.towerTypes[planet.tower.towerName].Levels.Count)
        {
            var currentLevel = PrefabManager.towerTypes[planet.tower.towerName].Levels[planet.tower.level - 1];
            var nextLevel = PrefabManager.towerTypes[planet.tower.towerName].Levels[planet.tower.level];
            CanvasManager.instance.mainGui.SetTowerStats(currentLevel, nextLevel, PrefabManager.towerTypes[planet.tower.towerName].Name);
        }
        else
        {
            CanvasManager.instance.mainGui.SetTowerStats(PrefabManager.towerTypes[planet.tower.towerName].Levels[planet.tower.level - 1], PrefabManager.towerTypes[planet.tower.towerName].Name);
        }
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(false);
    }
}
