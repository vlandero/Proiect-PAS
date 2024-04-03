using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private Vector3 direction;

    public void SetTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        this.direction = direction;
        transform.rotation = Quaternion.LookRotation(direction);
        //transform.LookAt(target.transform);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
