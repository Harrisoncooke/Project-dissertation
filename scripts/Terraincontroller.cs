using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public int width = 100;
    public int height = 100;
    public float scale = 15f; // Lower scale for larger biomes

    private TerrainModel model;
    private TerrainView view;

    void Start() // Generate a map and draw it using the TerrainView class
    {
        model = new TerrainModel(width, height);
        view = GetComponent<TerrainView>();

        void GenerateMap() // Generate a map using Perlin noise to create biomes and add rivers and beaches to the map
        {
            // Use a random seed for Perlin noise
            float seed = Random.Range(0f, 100f);

            for (int x = 0; x < model.width; x++)
            {
                for (int y = 0; y < model.height; y++)
                {
                    float xCoord = (float)x / model.width * scale + seed;
                    float yCoord = (float)y / model.height * scale + seed;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);

                    if (sample < 0.2f)
                    {
                        model.map[x, y] = 6; // Ocean
                    }
                    else if (sample < 0.3f)
                    {
                        model.map[x, y] = 3; // Desert
                    }
                    else if (sample < 0.5f)
                    {
                        model.map[x, y] = 3; // Desert
                    }
                    else if (sample < 0.7f)
                    {
                        model.map[x, y] = 4; // Forest
                    }
                    else if (sample < 0.9f)
                    {
                        model.map[x, y] = 1; // Floor
                    }
                    else
                    {
                        model.map[x, y] = 2; // Mountain
                    }
                }
            }

            // Add rivers
            AddRivers();

            // Add beaches
            AddBeaches();

            // Add roads and settlements
            AddRoadsAndSettlements();


        }
        HandlePreviewObjects(); // Handle all preview objects before generating the map

        GenerateMap();
        view.DrawMap(model);
    }

    void HandlePreviewObjects() // hides preview objects off-screen
    {
        // Find all preview objects by tag
        GameObject[] previewObjects = GameObject.FindGameObjectsWithTag("Preview");

        foreach (GameObject previewObject in previewObjects)
        {
            if (previewObject != null)
            {
                // Reposition to off-screen
                previewObject.transform.position = new Vector3(-1000, -1000, -1000);
            }
        }
    }

    void AddBeaches() // Add beaches next to water by changing floor tiles to beach tiles if they are next to water tiles
    {
        for (int x = 0; x < model.width; x++)
        {
            for (int y = 0; y < model.height; y++)
            {
                if (model.map[x, y] == 3 || model.map[x, y] == 4 || model.map[x, y] == 1 || model.map[x, y] == 2)
                {
                    if (IsNextToWater(x, y)) // Check if the tile is next to water
                    {
                        model.map[x, y] = 5; // Beach
                    }
                }
            }
        }
    }

    bool IsNextToWater(int x, int y) // Check if a tile is next to water (ocean or river)
    {
        int[,] directions = new int[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newX = x + directions[i, 0]; // Check each direction to see if there is water
            int newY = y + directions[i, 1];
            if (newX >= 0 && newX < model.width && newY >= 0 && newY < model.height)
            {
                if (model.map[newX, newY] == 6 || model.map[newX, newY] == 7) // Ocean or river
                {
                    return true;
                }
            }
        }
        return false;
    }

    void AddRivers() // Add rivers to the map by creating a path of river tiles from the ocean to another body of water
    {
        for (int i = 0; i < 5; i++)
        {
            // Find a starting point in the ocean
            int x = Random.Range(0, model.width);
            int y = Random.Range(0, model.height);
            while (model.map[x, y] != 6) // Ensure starting point is in the ocean
            {
                x = Random.Range(0, model.width);
                y = Random.Range(0, model.height);
            }

            int direction = Random.Range(0, 4);
            for (int j = 0; j < 200; j++) // Increase the number of steps for river length
            {
                model.map[x, y] = 7; // River

                // Randomly change direction occasionally to create a more natural river path
                if (Random.Range(0, 10) < 2)
                {
                    direction = Random.Range(0, 4);
                }

                switch (direction)
                {
                    case 0: // Up
                        y = Mathf.Clamp(y + 1, 0, model.height - 1);
                        break;
                    case 1: // Down
                        y = Mathf.Clamp(y - 1, 0, model.height - 1);
                        break;
                    case 2: // Left
                        x = Mathf.Clamp(x - 1, 0, model.width - 1);
                        break;
                    case 3: // Right
                        x = Mathf.Clamp(x + 1, 0, model.width - 1);
                        break;
                }

                // Stop if the river reaches another body of water
                if (model.map[x, y] == 6 || model.map[x, y] == 7)
                {
                    break;
                }
            }
        }
    }

    void AddRoadsAndSettlements() // Add roads and settlements to the map
    {
        for (int i = 0; i < 3; i++) //generates a few roads and settlements
        {
            // Find a start point on the map
            int x = Random.Range(0, model.width);
            int y = Random.Range(0, model.height);

            int direction = Random.Range(0, 4);
            for (int j = 0; j < 100; j++) // Length of the road
            {
                // Ensure the tile is not water or mountain before placing a road
                if (model.map[x, y] != 6 && model.map[x, y] != 2)
                {
                    model.map[x, y] = 8; // Road

                    // Randomly place settlements along the road
                    if (Random.Range(0, 10) < 2)
                    {
                        // Ensure the settlement is placed on a road tile
                        if (model.map[x, y] == 8)
                        {
                            model.map[x, y] = 9; // Settlement
                        }
                    }
                }

                // Randomly change direction to create a more natural looking road
                if (Random.Range(0, 10) < 2)
                {
                    direction = Random.Range(0, 4);
                }

                switch (direction)
                {
                    case 0: // Up
                        y = Mathf.Clamp(y + 1, 0, model.height - 1);
                        break;
                    case 1: // Down
                        y = Mathf.Clamp(y - 1, 0, model.height - 1);
                        break;
                    case 2: // Left
                        x = Mathf.Clamp(x - 1, 0, model.width - 1);
                        break;
                    case 3: // Right
                        x = Mathf.Clamp(x + 1, 0, model.width - 1);
                        break;
                }
            }
        }
    }
}

