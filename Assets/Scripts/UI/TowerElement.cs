using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI towerName;

    [HideInInspector] public TowerNameType towerType;

    public void UpdateName(TowerNameType t)
    {
        towerName.text = PrefabManager.towerTypes[t].Name;
        towerType = t;
    }

    public void Select()
    {
        transform.parent.GetComponent<TowerPanelContent>().Select(towerType);
        CanvasManager.instance.mainGui.ToggleTowerStatsPanel(false);
    }
}
