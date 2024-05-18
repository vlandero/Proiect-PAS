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
    public AudioSource sound;
    public MeshRenderer renderer;
    private Vector3 direction;

    public void SetTarget(GameObject target)
    {
        direction = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Start()
    {
        sound.Play();
        StartCoroutine(DestroyAfterTime(timeToLive));
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction, Space.World);
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject, sound.clip.length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(attackTarget == AttackTarget.Tower)
        {
            if(other.CompareTag("Sun"))
            {
                other.GetComponent<Sun>().TakeDamage(damage);
                renderer.enabled = false;
            }
            else if(other.CompareTag("Tower"))
            {
                other.GetComponent<Tower>().TakeDamage(damage);
                renderer.enabled = false;
            }
        }
        else if(attackTarget == AttackTarget.Spaceship)
        {
            if (other.CompareTag("Enemy") && !hit)
            {
                var en = other.GetComponent<Enemy>();
                if (!en.IsDying)
                {
                    en.TakeDamage(damage);
                    hit = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
