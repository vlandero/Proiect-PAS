using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float timeToLive = 5f;

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
        if (other.CompareTag("Enemy"))
        {
            return;
        }
        Debug.Log("Bullet hit " + other.name);
        Destroy(gameObject);
    }
}
