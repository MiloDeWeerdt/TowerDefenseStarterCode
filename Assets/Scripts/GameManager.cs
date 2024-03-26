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
        // Je kunt niet bouwen als er geen site is geselecteerd
        if (selectedSite == null)
        {
            Debug.LogError("No construction site selected.");
            return;
        }

        // Selecteer de juiste lijst op basis van het torentype
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

        // Gebruik een switch met het niveau om een GameObject-toren te maken
        GameObject towerPrefab = towerList[(int)level];
        if (towerPrefab == null)
        {
            Debug.LogError("Tower prefab is null for type: " + type + " and level: " + level);
            return;
        }
        // Haal de positie van de ConstructionSite op
        Vector3 buildPosition = selectedSite.GetBuildPosition();

        GameObject towerInstance = Instantiate(towerPrefab, buildPosition, Quaternion.identity);

        // Configureer de geselecteerde site om de toren in te stellen
        selectedSite.SetTower(towerInstance, level, type); // Voeg level en type toe als
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
        // Stel de startwaarden in
        credits = 500;
        health = 10;
        currentWave = 0; // Initialize with 0 to start with the first wave
        TopMenu.UpdateTopMenuLabels(credits, health, currentWave + 1); // Update the labels with the correct wave index
    }

    public int GetCurrentWave()
    {
        return currentWave - 1; // Geef de huidige golfindex terug
    }
    public int GetCredits()
    {
        // Return het huidige aantal credits
        return credits;
    }
    public void RemoveCredits(int amount)
    {
        // Verminder credits
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }
}

