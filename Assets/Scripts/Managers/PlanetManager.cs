using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private static PlanetManager instance;
    public static PlanetManager Instance => instance;

    public Planet[] planets;
    public Sun sun;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Sun>();
        GameObject planetsObj = GameObject.FindGameObjectWithTag("Planets");
        planets = new Planet[planetsObj.transform.childCount];
        for (int i = 0; i < planetsObj.transform.childCount; i++)
        {
            planets[i] = planetsObj.transform.GetChild(i).gameObject.GetComponent<Planet>();
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

}
