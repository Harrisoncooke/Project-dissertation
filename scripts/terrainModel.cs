using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModel // Model class for the terrain
{
    public int[,] map;
    public int width;
    public int height;

    public TerrainModel(int width, int height) // Constructor for the TerrainModel class
    {
        this.width = width; // Set the width and height before creating a 2D array to store the information
        this.height = height;
        map = new int[width, height];
    }
}

