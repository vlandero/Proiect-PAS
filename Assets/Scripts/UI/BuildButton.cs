using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TowerElement towerElement;
    private void Start()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(TowerData.TowerTypes[towerElement.towerType].Levels[0]);
        Debug.Log("Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel();
        Debug.Log("Pointer Exit");
    }
}
