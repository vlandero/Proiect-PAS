using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected int damage;
    protected int hp;
    protected int maxHp;
    protected int range;
    protected float attackSpeed;
    protected float moveSpeed;
    protected float healSpeed;
    protected int healPower;

    protected int stoppingDistance = 2;
    public EnemyNameType enemyName;
    [Header("References")]
    public Transform targetPoint;
    [SerializeField] private TowerHp enemyHpUi;
    

    protected Planet[] planets;
    protected Sun sun;
    protected EnemyPathNavMesh enemyPath;

    protected Material dissolveMaterialInstance;
    protected float dissolveAmount = 0f;
    protected float dissolveAmountTarget = 1f;
    protected bool isDying = false;

    [Header("Dissolve")]
    [SerializeField] protected GameObject mesh;
    [SerializeField] protected float dissolveSpeed = 1f;
    [SerializeField] protected Material dissolveMaterial;

    public bool IsDying { get { return isDying; } }

    public void Start()
    {
        planets = PlanetManager.Instance.planets;
        sun = PlanetManager.Instance.sun;
        enemyPath = GetComponent<EnemyPathNavMesh>();
        enemyPath.stoppingDistance = stoppingDistance;

        EnemyType data = PrefabManager.enemyTypes[enemyName];
        damage = data.Damage;
        hp = data.Hp;
        maxHp = data.Hp;
        range = data.Range;
        attackSpeed = data.AttackSpeed;
        moveSpeed = data.MoveSpeed;
        healSpeed = data.HealSpeed;
        healPower = data.HealPower;

        enemyPath.moveSpeed = moveSpeed;
        enemyPath.agent.speed = moveSpeed;
        dissolveMaterialInstance = Instantiate(dissolveMaterial);
        var meshChildren = mesh.transform.childCount;
        for(int i = 0; i < meshChildren; i++)
        {
            mesh.transform.GetChild(i).GetComponent<Renderer>().material = dissolveMaterialInstance;
        }
        dissolveMaterialInstance.SetFloat("_Dissolve", dissolveAmount);

        StartCoroutine(ShootAtTarget());
        StartCoroutine(HealOverTime());
    }

    private IEnumerator HealOverTime()
    {
        while (!isDying)
        {
            yield return new WaitForSeconds(healSpeed);
            Heal(healPower);
        }
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
        StopCoroutine(HealOverTime());
        LevelBalanceManager.Instance.UpdateCoins(PrefabManager.enemyTypes[enemyName].Reward);
        EnemyManager.Instance.enemies.Remove(this);
        enemyPath.moveSpeed = 0;
        enemyPath.stopped = true;
        enemyPath.agent.enabled = false;
        moveSpeed = 0;
        isDying = true;
    }
}
