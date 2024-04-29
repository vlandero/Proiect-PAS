using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private GameObject addTowerButton;
    [SerializeField] private GameObject planetMesh;
    [SerializeField] private GameObject selectTowerButton;
    [SerializeField] private GameObject towerCanvas;
    [SerializeField] private GameObject upgradeTowerButton;

    public UpgradeTowerIcon upgradeTowerIcon;

    public Tower tower = null;

    private Renderer planetMeshRenderer;

    private void Start()
    {
        planetMeshRenderer = planetMesh.GetComponent<Renderer>();
        addTowerButton.SetActive(true);
        selectTowerButton.SetActive(false);
        upgradeTowerButton.SetActive(false);
    }

    public void CreateTower(TowerNameType t)
    {
        GameObject towerPrefab = PrefabManager.towerPrefabs[t];
        GameObject tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        tower.transform.SetParent(transform);
        float planetHeight = planetMeshRenderer.bounds.size.y;
        float towerHeight = tower.GetComponent<Renderer>().bounds.size.y;
        tower.transform.localPosition = planetMesh.transform.localPosition + new Vector3(0, planetHeight / 2 + towerHeight / 2, 0);
        this.tower = tower.GetComponent<Tower>();
        addTowerButton.SetActive(false);
        selectTowerButton.SetActive(false);
        upgradeTowerButton.SetActive(true);
        if (TowerData.TowerTypes[t].Levels.Count > 1)
        {
            upgradeTowerIcon.SetUpgrade();
        }
        else
        {
            upgradeTowerIcon.SetLock();
        }
    }

    public void DestroyTower()
    {
        tower = null;
        towerCanvas.SetActive(true);
        addTowerButton.SetActive(true);
        selectTowerButton.SetActive(false);
        upgradeTowerButton.SetActive(false);
    }

    public bool HasTower()
    {
        return tower != null;
    }

    public void ShowAddTowerCanvas()
    {
        addTowerButton.SetActive(true);
        selectTowerButton.SetActive(false);
    }

    public void ShowSelectTowerCanvas()
    {
        addTowerButton.SetActive(false);
        selectTowerButton.SetActive(true);
    }

    public void UpgradeTower()
    {
        if(tower != null && tower.level < TowerData.TowerTypes[tower.towerName].Levels.Count)
        {
            tower.Upgrade();
            CanvasManager.instance.mainGui.SetTowerStats(TowerData.TowerTypes[tower.towerName].Levels[tower.level - 1]);
        }
    }
}
