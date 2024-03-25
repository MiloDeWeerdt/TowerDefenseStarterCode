using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite
{
    
        public Vector3Int TilePosition { get; private set; }
        public Vector3 WorldPosition { get; private set; }
        public SiteLevel Level { get; private set; }
        public TowerType TowerType { get; private set; }

        private GameObject tower;
       
        public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
        {
            TilePosition = tilePosition;
            WorldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.5f, worldPosition.z); // Pas de y-waarde aan

            tower = null;
        }

        public void SetTower(GameObject newTower, SiteLevel newLevel, TowerType newType)
        {
            if (tower != null)
            {
                GameObject.Destroy(tower);
            }

            tower = newTower;
            Level = newLevel;
            TowerType = newType;
        
        }
    public Vector3 GetBuildPosition()
    {
        return WorldPosition; // Hier retourneer je de positie waar de toren moet worden gebouwd
    }
    public TowerType GetTowerType()
    {
        if (tower != null)
        {
            // Hieronder wordt aangenomen dat TowerType is opgeslagen als een eigenschap van het tower GameObject
            Tower towerScript = tower.GetComponent<Tower>();
            if (towerScript != null)
            {
                return towerScript.TowerType;
            }
        }
        return TowerType.Onbebouwd;
    }


}
