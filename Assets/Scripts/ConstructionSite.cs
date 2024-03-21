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
            // Wijs de tilePosition en worldPosition toe
            TilePosition = tilePosition;
            WorldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.5f, worldPosition.z); // Pas de y-waarde aan

            // Stel tower in op null
            tower = null;
        }

        public void SetTower(GameObject newTower, SiteLevel newLevel, TowerType newType)
        {
            // Controleer of er al een tower is
            if (tower != null)
            {
                // Verwijder de bestaande tower voordat je de nieuwe toewijst
                GameObject.Destroy(tower);
            }

            // Wijs de nieuwe tower toe
            tower = newTower;
            Level = newLevel;
            TowerType = newType;
        }
    
}
