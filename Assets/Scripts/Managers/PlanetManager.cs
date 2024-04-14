using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private static PlanetManager instance;
    public static PlanetManager Instance => instance;

    public GameObject[] planets; // de inlocuit cu clasa planetei
    public GameObject sun;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        sun = GameObject.FindGameObjectWithTag("Sun");
        GameObject planetsObj = GameObject.FindGameObjectWithTag("Planets");
        planets = new GameObject[planetsObj.transform.childCount];
        for (int i = 0; i < planetsObj.transform.childCount; i++)
        {
            planets[i] = planetsObj.transform.GetChild(i).gameObject;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

}
