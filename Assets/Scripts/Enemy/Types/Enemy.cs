using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyNameType enemyName;
    public Transform targetPoint;
    [SerializeField] private TowerHp enemyHpUi;
    [HideInInspector] protected int damage;
    [HideInInspector] protected int hp;
    [HideInInspector] protected int maxHp;
    [HideInInspector] protected int range;
    [HideInInspector] protected float attackSpeed;
    [HideInInspector] protected float moveSpeed;

    [HideInInspector] protected int stoppingDistance = 2;

    protected Planet[] planets;
    protected Sun sun;
    protected EnemyPath enemyPath;

    [SerializeField] protected Material dissolveMaterial;
    protected Material dissolveMaterialInstance;
    [SerializeField] protected float dissolveSpeed = 1f;
    [SerializeField] protected GameObject mesh;
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
        maxHp = data.Hp;
        range = data.Range;
        attackSpeed = data.AttackSpeed;
        moveSpeed = data.MoveSpeed;

        enemyPath.moveSpeed = moveSpeed;
        dissolveMaterialInstance = Instantiate(dissolveMaterial);
        var meshChildren = mesh.transform.childCount;
        for(int i = 0; i < meshChildren; i++)
        {
            mesh.transform.GetChild(i).GetComponent<Renderer>().material = dissolveMaterialInstance;
        }
        dissolveMaterialInstance.SetFloat("_Dissolve", dissolveAmount);

        StartCoroutine(ShootAtTarget());
    }

    public void Update()
    {
        if(isDying)
        {
            dissolveAmount = Mathf.Lerp(dissolveAmount, dissolveAmountTarget, Time.deltaTime * dissolveSpeed);
            dissolveMaterialInstance.SetFloat("_Dissolve", dissolveAmount);
            if(dissolveAmount >= 0.95f)
            {
                Destroy(dissolveMaterialInstance);
                Destroy(gameObject);
            }
            return;
        }
    }

    protected abstract IEnumerator ShootAtTarget();

    public void TakeDamage(int damage)
    {
        hp -= damage;
        enemyHpUi.UpdateHp((float)hp / (float)maxHp);
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        hp = Mathf.Min(hp + healAmount, maxHp);
        enemyHpUi.UpdateHp((float)hp / (float)maxHp);
    }

    public void Die()
    {
        if(isDying)
        {
            return;
        }
        StopCoroutine(ShootAtTarget());
        LevelBalanceManager.Instance.UpdateCoins(EnemyData.enemyTypes[enemyName].Reward);
        EnemyManager.Instance.enemies.Remove(this);
        enemyPath.moveSpeed = 0;
        moveSpeed = 0;
        isDying = true;
    }
}
