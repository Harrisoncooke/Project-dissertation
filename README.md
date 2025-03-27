Unity Terrain Generation Project

This project generates a terrain map using Perlin noise and includes camera controls for navigation. Follow the steps below to set up the project in a new empty Unity project.

Prerequisites

- Unity 2020.3 or later
- Basic knowledge of Unity and C#

Project Setup

1. Create a New Unity Project
   - Open Unity Hub.
   - Click on the `New` button.
   - Select the `3D` template.
   - Name your project and choose a location to save it.
   - Click on `Create`.

2. Import the Scripts
   - Create a folder named `Scripts` in the `Assets` directory.
   - Copy the following scripts into the `Assets/Scripts` folder:
     - `TerrainController.cs`
     - `TerrainModel.cs`
     - `TerrainView.cs`
     - `PerlinNoise.cs`
     - `CameraController.cs`
     - `CameraMovementScript.cs`

3. Create the Terrain Tiles
   - Create a folder named `Prefabs` in the `Assets` directory.
   - Create the following GameObjects in the scene and save them as prefabs in the `Assets/Prefabs` folder:
     - `FloorTile`
     - `WallTile`
     - `MountainTile`
     - `DesertTile`
     - `ForestTile`
     - `BeachTile`
     - `OceanTile`
     - `RiverTile`
     - `RoadTile`
     - `SettlementTile`

4. Set Up the Scene
   - Create an empty GameObject named `TerrainManager`.
   - Attach the `TerrainController` script to the `TerrainManager` GameObject.
   - Add the `TerrainView` component to the `TerrainManager` GameObject.
   - Assign the corresponding prefabs to the fields in both the `TerrainController` and `TerrainView` scripts.

5. Set Up the Camera
   - Create a new Camera in the scene.
   - Attach the `CameraController` script to the Camera.
   - Optionally, attach the `CameraMovementScript` script to the Camera for additional movement controls.

6. Run the Project
   - Press the `Play` button in the Unity Editor to generate and view the terrain.

Script Descriptions

- TerrainController.cs: Manages the generation of the terrain map using Perlin noise and handles the addition of rivers, beaches, roads, and settlements. It acts as the main controller, binding the terrain generation logic with the view.
- TerrainModel.cs: Represents the model for the terrain, including the map data and dimensions.
- TerrainView.cs: Handles the visualization of the terrain map by instantiating the appropriate tile prefabs.
- PerlinNoise.cs: Provides a Perlin noise implementation for generating terrain features.
- CameraController.cs: Allows panning and zooming of the camera.
- CameraMovementScript.cs: Provides additional camera movement controls using keyboard input.

Notes

- Ensure that all prefabs are correctly assigned to the respective fields in the scripts.
- Adjust the `width`, `height`, and `scale` parameters in the `TerrainController` script to customize the terrain generation.
