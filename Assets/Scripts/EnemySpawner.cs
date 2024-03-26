using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
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
        script.path = path;
        script.target= Path1[1];
        GameManager.Instance.AddInGameEnemy();
    }
    private int ufoCounter = 0;

    public void StartWave(int number)
    {
        ufoCounter = 0;

        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
        }
    }

    public void StartWave1()
    {
        ufoCounter++;

        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 30)
        {
            SpawnEnemy(0, Path.Path1);
        }
        else
        {
            SpawnEnemy(1, Path.Path1);
        }

        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1");
            GameManager.Instance.EndWave();
        }
    }
    void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }
    private void SpawnTester()
    {

        SpawnEnemy(0, Path.Path1);

    }
    public GameObject RequestTarget(Path path, int index)

    {
        List<GameObject> selectedPath = path == Path.Path1 ? Path1 : Path2;

        if (index < selectedPath.Count)
        {
            return selectedPath[index];
        }
        else
        {
            return null;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
