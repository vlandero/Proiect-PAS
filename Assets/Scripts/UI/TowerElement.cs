using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI towerName;
    TowerNameType towerType;

    public void UpdateName(TowerNameType t)
    {
        towerName.text = TowerData.TowerTypes[t].Name;
        towerType = t;
    }

    public void Select()
    {
        transform.parent.GetComponent<TowerPanelContent>().Select(towerType);
    }
}
