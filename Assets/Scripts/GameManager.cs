using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject TowerMenu;
    private TowerMenu towerMenu;
    public List<GameObject> Archers = new List<GameObject>();
    public List<GameObject> Swords = new List<GameObject>();
    public List<GameObject> Wizards = new List<GameObject>();
    private ConstructionSite selectedSite;
    private int health;
    private int credits;
    private int wave;
    private int currentWave;
    public TopMenu topMenu;
    private bool waveActive = false;
    // Start is called before the first frame update 
    void Start()
    {
        if (TowerMenu != null)
        {
            towerMenu = TowerMenu.GetComponent<TowerMenu>();
        }
        else
        {
            Debug.LogError("TowerMenu reference is not set in GameManager.");
        }
        StartGame();

    }
    public void SelectSite(ConstructionSite site)
    {
        if (towerMenu == null)
        {
            Debug.LogError("TowerMenu is not initialized in GameManager.");
            return;
        }

        this.selectedSite = site;
        towerMenu.SetSite(site);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Build(TowerType type, SiteLevel level)
    {
        if (selectedSite == null)
        {
            Debug.LogError("No construction site selected.");
            return;
        }
        List<GameObject> towerList = null;
        switch (type)
        {
            case TowerType.Archer:
                towerList = Archers;
                break;
            case TowerType.Sword:
                towerList = Swords;
                break;
            case TowerType.Wizard:
                towerList = Wizards;
                break;
        }
        if ((int)level >= towerList.Count || (int)level < 0)
        {
            Debug.LogWarning("Invalid tower level selected for the tower type: " + type);
            return;
        }
        GameObject towerPrefab = towerList[(int)level];
        if (towerPrefab == null)
        {
            Debug.LogError("Tower prefab is null for type: " + type + " and level: " + level);
            return;
        }
        Vector3 buildPosition = selectedSite.GetBuildPosition();
        GameObject towerInstance = Instantiate(towerPrefab, buildPosition, Quaternion.identity);
        selectedSite.SetTower(towerInstance, level, type); 
        towerMenu.SetSite(null);
    }
    public void DestroyTower()
    {
        if (selectedSite == null)
        {
            Debug.LogError("Er is geen bouwplaats geselecteerd. Kan de toren niet verwijderen.");
            return;
        }
    }
    

    
    public void StartGame()
    {
        credits = 200;
        health = 10;
        currentWave = 0;
        UpdateTopMenuLabels();
        waveActive = false;
    }

    public void AttackGate()
    {
        health--;
        UpdateTopMenuLabels();
    }

    public void AddCredits(int amount)
    {
        credits += amount;
        UpdateTopMenuLabels();
        EvaluateTowerMenu();
    }

    public void RemoveCredits(int amount)
    {
        credits -= amount;
        UpdateTopMenuLabels();
        EvaluateTowerMenu();
    }
    public void StartWave()
    {
        currentWave++;
        TopMenu.UpdateWaveLabel("Wave: " + (currentWave + 1));
        waveActive = true;
        EnemySpawner.Instance.StartWave(currentWave);
    }

    public void EndWave()
    {
        waveActive = false;
        TopMenu.EnableWaveButton();
    }

    public bool IsWaveActive()
    {
        return waveActive;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    
    public int GetCredits()
    {
        return credits;
    }

    public int GetCost(TowerType type, SiteLevel level, bool selling = false)
    {
        int cost = 0;
        switch (type)
        {
            case TowerType.Archer:
                cost = selling ? (int)(Archers[(int)level].GetComponent<Tower>().sellValue) : (int)(Archers[(int)level].GetComponent<Tower>().cost);
                break;
            case TowerType.Sword:
                cost = selling ? (int)(Swords[(int)level].GetComponent<Tower>().sellValue) : (int)(Swords[(int)level].GetComponent<Tower>().cost);
                break;
            case TowerType.Wizard:
                cost = selling ? (int)(Wizards[(int)level].GetComponent<Tower>().sellValue) : (int)(Wizards[(int)level].GetComponent<Tower>().cost);
                break;
        }
        return cost;
    }

    public void EnemyReachedEnd(int points)
    {
        AttackGate();
        AddCredits(points);
    }

    private void UpdateTopMenuLabels()
    {
        topMenu.UpdateTopMenuLabels(credits, health, currentWave + 1);
    }

    private void EvaluateTowerMenu()
    {
        towerMenu.EvaluateMenu(); 
    }
}

