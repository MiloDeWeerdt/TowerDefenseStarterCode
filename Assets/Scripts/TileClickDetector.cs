using System.Collections.Generic;

using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Tilemaps;



public class TileClickDetector : MonoBehaviour

{

    public Camera cam; 

    public Tilemap tilemap; 



    public TileBase SelectedTile { get; private set; }

    public Vector3 SelectedPosition { get; private set; }



    private List<ConstructionSite> sites = new List<ConstructionSite>();



    public ConstructionSite SelectedSite { get; private set; }



    private void Start()

    {

        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)

        {

            for (int y = bounds.yMin; y < bounds.yMax; y++)

            {

                for (int z = bounds.zMin; z < bounds.zMax; z++)

                {

                    Vector3Int cellPosition = new Vector3Int(x, y, z);

                    TileBase foundTile = tilemap.GetTile(cellPosition);

                    if (foundTile != null && foundTile.name == "buildingPlaceGrass")

                    {

                        sites.Add(new ConstructionSite(cellPosition, tilemap.CellToWorld(cellPosition)));

                    }

                }

            }

        }

    }



    // Update is called once per frame 

    void Update()

    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            DetectTileClicked();
        }
    }
    void DetectTileClicked()

    {

         

        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPos.z = 0;
        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

        cellPosition.z = 0;
        TileBase clickedTile = tilemap.GetTile(cellPosition);



        if (clickedTile != null)

        {
            if (clickedTile.name == "buildingPlaceGrass")

            {

                SelectedSite = null;

                foreach (var site in sites)

                {

                    if (cellPosition == site.TilePosition)

                    {

                        SelectedSite = site;

                        break;

                    }

                }

            }

            else

            {

                SelectedSite = null;

            }

        }

        else

        {

            SelectedSite = null;

        }


        GameManager.Instance.SelectSite(SelectedSite);
    }

}