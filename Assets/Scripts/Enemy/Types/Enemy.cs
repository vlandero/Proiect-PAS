using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyNameType enemyName;
    [HideInInspector] public int damage;
    [HideInInspector] public int hp;
    [HideInInspector] public int range;
    [HideInInspector] public float attackSpeed;
    [HideInInspector] public float moveSpeed;

    [HideInInspector] public int stoppingDistance = 2;

    protected GameObject[] planets;
    protected GameObject sun;
    protected EnemyPath enemyPath;

    public void Start()
    {
        planets = PlanetManager.Instance.planets;
        sun = PlanetManager.Instance.sun;
        enemyPath = GetComponent<EnemyPath>();
        enemyPath.moveSpeed = moveSpeed;
        enemyPath.stoppingDistance = stoppingDistance;

        EnemyType data = EnemyData.enemyTypes[enemyName];
        damage = data.Damage;
        hp = data.Hp;
        range = data.Range;
        attackSpeed = data.AttackSpeed;
        moveSpeed = data.MoveSpeed;

        StartCoroutine(ShootAtTarget());
    }

    protected abstract IEnumerator ShootAtTarget();
}
