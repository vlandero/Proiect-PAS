using System.Collections;
using UnityEngine;

public enum AttackTarget
{
    Spaceship,
    Tower
}

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float timeToLive = 5f;
    public int damage = 0;
    public AttackTarget attackTarget;
    public bool hit = false;

    private Vector3 direction;
    public void SetTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        this.direction = direction;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Start()
    {
        StartCoroutine(DestroyAfterTime(timeToLive));
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction, Space.World);
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(attackTarget == AttackTarget.Tower)
        {
            if(other.CompareTag("Sun"))
            {
                other.GetComponent<Sun>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if(other.CompareTag("Tower"))
            {
                other.GetComponent<Tower>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if(attackTarget == AttackTarget.Spaceship)
        {
            if (other.CompareTag("Enemy") && !hit)
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
                hit = true;
                Destroy(gameObject);
            }
        }
    }
}
