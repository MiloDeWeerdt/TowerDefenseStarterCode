using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    // Start is called before the first frame update
    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> Enemies= new List<GameObject>();
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void SpawnEnemy(int type, Path path)
    {
        var newEnemy = Instantiate(Enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);
        var script = newEnemy.GetComponentInParent<Enemy>();
        // set hier het path en target voor je enemy in 
        path=path.gameObject;
    }
    private void SpawnTester()
    {

        SpawnEnemy(0, Path.Path1);

    }
    void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }
    public GameObject RequestTarget(Path path, int index)

    {
        if(path == Path.Path1)
        {
            if (index < path1Waypoints.Length - 1)
            {
                return path1Waypoints[index + 1];
            }
            else
            {
                return null;
            }
        }
        else
        {
            if (index < path2Waypoints.Length - 1)
            {
                return path2Waypoints[index + 1];
            }
            else
            {
                return null;
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
