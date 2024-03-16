using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 moveDirection = (target.position - transform.position).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= 0.2f)
        {
            DealDamage();
            Destroy(gameObject);
        }
    }

    void DealDamage()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}


