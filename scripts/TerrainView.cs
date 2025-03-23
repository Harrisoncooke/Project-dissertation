using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainView : MonoBehaviour
{
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject mountainTile;
    public GameObject desertTile;
    public GameObject forestTile;
    public GameObject beachTile;
    public GameObject oceanTile;
    public GameObject riverTile;
    public GameObject roadTile;
    public GameObject settlementTile;

    public void DrawMap(TerrainModel model) // Draw the map using the information from the TerrainModel class
    {
        Vector3 offset = new Vector3(model.width / 2, model.height / 2, 0);
        for (int x = 0; x < model.width; x++)
        {
            for (int y = 0; y < model.height; y++) // Loop through the 2D array and instantiate the correct tile based on the value
            {
                GameObject tile = null;
                switch (model.map[x, y])
                {
                    case 1:
                        tile = floorTile;
                        break;
                    case 2:
                        tile = mountainTile;
                        break;
                    case 3:
                        tile = desertTile;
                        break;
                    case 4:
                        tile = forestTile;
                        break;
                    case 5:
                        tile = beachTile;
                        break;
                    case 6:
                        tile = oceanTile;
                        break;
                    case 7:
                        tile = riverTile;
                        break;
                    case 8:
                        tile = roadTile;
                        break;
                    case 9:
                        tile = settlementTile;
                        break;
                    default:
                        tile = wallTile;
                        break;
                }
                Instantiate(tile, new Vector3(x, y, 0) - offset, Quaternion.identity);
            }
        }
    }
}
   
