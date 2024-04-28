using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private GameObject addTowerButton;
    [SerializeField] private GameObject planetMesh;
    [SerializeField] private GameObject addTowerCanvas;
    [SerializeField] private GameObject selectTowerCanvas;
    [SerializeField] private GameObject towerCanvas;

    public Tower tower = null;

    private Renderer planetMeshRenderer;

    private void Start()
    {
        planetMeshRenderer = planetMesh.GetComponent<Renderer>();
        addTowerButton.SetActive(true);
        selectTowerCanvas.SetActive(false);
    }

    public void CreateTower(TowerNameType t)
    {
        towerCanvas.SetActive(false);
        GameObject towerPrefab = PrefabManager.towerPrefabs[t];
        GameObject tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        tower.transform.SetParent(transform);
        float planetHeight = planetMeshRenderer.bounds.size.y;
        float towerHeight = tower.GetComponent<Renderer>().bounds.size.y;
        tower.transform.localPosition = planetMesh.transform.localPosition + new Vector3(0, planetHeight / 2 + towerHeight / 2, 0);
        this.tower = tower.GetComponent<Tower>();
    }

    public void DestroyTower()
    {
        tower = null;
        towerCanvas.SetActive(true);
        addTowerCanvas.SetActive(true);
        selectTowerCanvas.SetActive(false);
    }

    public bool HasTower()
    {
        return tower != null;
    }

    public void ShowAddTowerCanvas()
    {
        addTowerCanvas.SetActive(true);
        selectTowerCanvas.SetActive(false);
    }

    public void ShowSelectTowerCanvas()
    {
        addTowerCanvas.SetActive(false);
        selectTowerCanvas.SetActive(true);
    }
}
