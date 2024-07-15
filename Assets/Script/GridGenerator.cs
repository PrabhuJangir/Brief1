using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // Prefab for the grid tiles
    public int gridWidth = 10; // Width of the grid
    public int gridHeight = 10; // Height of the grid
    public float tileSize = 1.0f; // Size of each tile
    public Color firstCube; // Color for the first set of cubes
    public Color secondCube; // Color for the second set of cubes

    void Start()
    {
        // Call GenerateGrid method when the script starts
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Loop through the grid dimensions
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Instantiate a new tile at the specified position
                GameObject tile = Instantiate(tilePrefab, new Vector3(x * tileSize, 0, y * tileSize), Quaternion.identity);

                // Add TileInfo component to the tile and set its information
                TileInfo tileComponent = tile.AddComponent<TileInfo>();
                tileComponent.SetTileInfo(x, y);

                // Set the name of the tile for easy identification
                tile.name = $"Tile_{x}_{y}";

                // Set the parent of the tile to the GridGenerator object
                tile.transform.parent = transform;

                // Set color based on checkerboard pattern
                if ((x + y) % 2 == 0)
                {
                    // Use firstCube color for even sums of x + y
                    tileComponent.SetColor(firstCube);
                }
                else
                {
                    // Use secondCube color for odd sums of x + y
                    tileComponent.SetColor(secondCube);
                }
            }
        }
    }
}
