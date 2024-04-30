using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPanelContent : MonoBehaviour
{
    [SerializeField] private SelectTowerPanel selectTowerPanel;
    public void Select(TowerNameType t)
    {
        selectTowerPanel.Select(t);
    }
}
