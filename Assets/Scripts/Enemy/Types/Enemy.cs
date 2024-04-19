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

    [SerializeField] protected Material dissolveMaterial;
    [SerializeField] protected float dissolveSpeed = 1f;
    protected float dissolveAmount = 0f;
    protected float dissolveAmountTarget = 1f;
    protected bool isDying = false;

    public bool IsDying { get { return isDying; } }

    public void Start()
    {
        planets = PlanetManager.Instance.planets;
        sun = PlanetManager.Instance.sun;
        enemyPath = GetComponent<EnemyPath>();
        enemyPath.stoppingDistance = stoppingDistance;

        EnemyType data = EnemyData.enemyTypes[enemyName];
        damage = data.Damage;
        hp = data.Hp;
        range = data.Range;
        attackSpeed = data.AttackSpeed;
        moveSpeed = data.MoveSpeed;

        enemyPath.moveSpeed = moveSpeed;
        dissolveMaterial.SetFloat("_Dissolve", dissolveAmount);

        StartCoroutine(ShootAtTarget());
    }

    public void Update()
    {
        if(isDying)
        {
            dissolveAmount = Mathf.Lerp(dissolveAmount, dissolveAmountTarget, Time.deltaTime * dissolveSpeed);
            dissolveMaterial.SetFloat("_Dissolve", dissolveAmount);
            if(dissolveAmount >= 0.95f)
            {
                Destroy(gameObject);
            }
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Die();
        }
    }

    protected abstract IEnumerator ShootAtTarget();

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        hp = Mathf.Min(hp + healAmount, EnemyData.enemyTypes[enemyName].Hp);
    }

    public void Die()
    {
        StopCoroutine(ShootAtTarget());
        enemyPath.moveSpeed = 0;
        moveSpeed = 0;
        isDying = true;
    }
}
