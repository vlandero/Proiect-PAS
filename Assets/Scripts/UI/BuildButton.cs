using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TowerElement towerElement;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CanvasManager.instance.mainGui.SetTowerStats(TowerData.TowerTypes[towerElement.towerType].Levels[0]);
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(false);
    }
}
