using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public int points = 1;
    public Path path { get; set; }
    public GameObject target { get; set; }
    private int pathIndex = 1;
    // Start is called before the first frame update
    
    void Start()
    {
       
    }

    // Update is called once per frame
    

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        { 
            target = EnemySpawner.Instance.RequestTarget(path, pathIndex);
            pathIndex++;
            if (target == null)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void DestroySelf()
    {
        GameManager.Instance.RemoveInGameEnemy();
        Destroy(gameObject);
    }
}
