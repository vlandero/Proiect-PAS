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

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buildSound;
    [SerializeField] private AudioClip upgradeSound;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip failToUpgradeSound;

    public UpgradeTowerIcon upgradeTowerIcon;

    public Tower tower = null;

    [SerializeField] public Renderer planetMeshRenderer;
    [Header("Stats")]
    public int orbitSpeed = 10;

    private Sun sunRef;

    private void Start()
    {
        planetMeshRenderer = planetMesh.GetComponent<Renderer>();
        addTowerButton.SetActive(true);
        selectTowerButton.SetActive(false);
        upgradeTowerButton.SetActive(false);

        sunRef = PlanetManager.Instance.sun;
    }

    private void Update()
    {
        RotateAroundSun();
    }

    private void RotateAroundSun()
    {
        if (sunRef != null)
        {

            transform.RotateAround(sunRef.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }

    public void CreateTower(TowerNameType t)
    {
        TowerType towerType = PrefabManager.towerTypes[t];
        if(LevelBalanceManager.Instance.coins < towerType.Levels[0].UpgradePrice)
        {
            return;
        }
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
        if (towerType.Levels.Count > 1)
        {
            upgradeTowerIcon.SetUpgrade();
        }
        else
        {
            upgradeTowerIcon.SetLock();
        }
        LevelBalanceManager.Instance.UpdateCoins(-towerType.Levels[0].UpgradePrice);
        audioSource.PlayOneShot(buildSound);
    }

    public void DestroyTower()
    {
        tower = null;
        towerCanvas.SetActive(true);
        addTowerButton.SetActive(true);
        selectTowerButton.SetActive(false);
        upgradeTowerButton.SetActive(false);
        audioSource.PlayOneShot(destroySound);
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
        if(tower != null && tower.level < PrefabManager.towerTypes[tower.towerName].Levels.Count)
        {
            bool hasUpgraded = tower.Upgrade();
            if (hasUpgraded)
            {
                audioSource.PlayOneShot(upgradeSound);
            }
            else
            {
                audioSource.PlayOneShot(failToUpgradeSound);
            }
            CanvasManager.instance.mainGui.SetTowerStats(PrefabManager.towerTypes[tower.towerName].Levels[tower.level - 1], PrefabManager.towerTypes[tower.towerName].Name);
        }
    }
}
