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
}
