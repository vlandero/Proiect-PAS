using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TowerElement towerElement;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CanvasManager.instance.mainGui.SetTowerStats(PrefabManager.towerTypes[towerElement.towerType].Levels[0], PrefabManager.towerTypes[towerElement.towerType].Name);
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(false);
    }
}
