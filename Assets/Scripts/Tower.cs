using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 1f; // Range within which the tower can detect and attack enemies 
    public float attackRate = 1f; // How often the tower attacks (attacks per second) 
    public int attackDamage = 1; // How much damage each attack does 
    public float attackSize = 1f; // How big the bullet looks 
    public GameObject bulletPrefab; // The bullet prefab the tower will shoot 
    public TowerType type; // the type of this tower 

    private float attackTimer = 0f; // Timer to keep track of attack rate
    public TowerType TowerType
    {
        get { return type; }
    }
    // Draw the attack range in the editor for easier debugging 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Update()
    {
        // Update the attackTimer
        attackTimer += Time.deltaTime;

        // If enough time has passed, check for enemies and attack
        if (attackTimer >= 1f / attackRate)
        {
            Attack();
            attackTimer = 0f; // Reset the timer
        }
    }

    void Attack()
    {
        // Find all enemies within the attack range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Create a bullet and set its properties
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f);

                // Set the target and damage of the bullet
                Projectile bulletScript = bullet.GetComponent<Projectile>();
                bulletScript.target = collider.transform;
                bulletScript.damage = attackDamage;

                // Break the loop after attacking one enemy per frame
                break;
            }
        }
    }
}
