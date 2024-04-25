using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private GameObject addTowerButton;
    [SerializeField] private GameObject planetMesh;
    [SerializeField] private GameObject addTowerCanvas;
    [SerializeField] private GameObject selectTowerCanvas;

    private Renderer planetMeshRenderer;

    private void Start()
    {
        planetMeshRenderer = planetMesh.GetComponent<Renderer>();
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Planet took " + damage + " damage");
    }

    public void CreateTower()
    {
        addTowerButton.SetActive(false);
        GameObject towerPrefab = PrefabManager.towerPrefabs[TowerNameType.Sentinel];
        GameObject tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        tower.transform.SetParent(transform);
        float planetHeight = planetMeshRenderer.bounds.size.y;
        float towerHeight = tower.GetComponent<Renderer>().bounds.size.y;
        tower.transform.localPosition = planetMesh.transform.localPosition + new Vector3(0, planetHeight / 2 + towerHeight / 2, 0);
    }
}
