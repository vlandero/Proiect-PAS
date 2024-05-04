using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectTowerPanel : MonoBehaviour
{
    [SerializeField] private GameObject towerElementPrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private Planet planet;

    private void Awake()
    {
        var towers = PrefabManager.towerTypes.Keys.ToArray();
        foreach (var tower in towers)
        {
            var towerElement = Instantiate(towerElementPrefab, content.transform);
            towerElement.transform.SetParent(content.transform);
            towerElement.GetComponent<TowerElement>().UpdateName(tower);
        }
    }

    public void Select(TowerNameType t)
    {
        planet.CreateTower(t);
    }
}
