using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float timeToLive = 5f;
    public int damage = 0;
    public AudioSource sound;
    public MeshRenderer renderer;

    private Vector3 direction;
    public void SetTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        this.direction = direction;
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
        if (other.CompareTag("Enemy"))
        {
            return;
        }
        if (other.GetComponent<Planet>() != null)
        {
            renderer.enabled = false;
        }
        if (other.GetComponent<SunDamage>() != null)
        {
            renderer.enabled = false;
        }
        other.GetComponent<Planet>()?.TakeDamage(damage);
        other.GetComponent<SunDamage>()?.TakeDamage(damage);
        Destroy(gameObject, sound.clip.length);
    }
}
