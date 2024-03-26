using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackRate = 1f; 
    public int attackDamage = 1; 
    public float attackSize = 1f; 
    public GameObject bulletPrefab; 
    public TowerType type;

    private float attackTimer = 0f; 
    public TowerType TowerType
    {
        get { return type; }
    } 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 1f / attackRate)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f);
                Projectile bulletScript = bullet.GetComponent<Projectile>();
                bulletScript.target = collider.transform;
                bulletScript.damage = attackDamage;
                break;
            }
        }
    }
}
