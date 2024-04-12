using System.Collections;
using UnityEngine;
// enum cu names si selectezi din el pentru a alege tipul de inamic
public abstract class Enemy : MonoBehaviour
{
    public int damage = 1;
    public int hp = 1;
    public int range = 10;
    public int attackSpeed = 1;
    public int bulletSpeed = 20;

    protected GameObject[] planets;
    protected GameObject sun;
    protected EnemyPath enemyPath;

    public void Start()
    {
        planets = PlanetManager.Instance.planets;
        sun = PlanetManager.Instance.sun;
        enemyPath = GetComponent<EnemyPath>();

        StartCoroutine(ShootAtTarget());
    }

    protected abstract IEnumerator ShootAtTarget();
}
