using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public GameObject tilePrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float tileSize = 1.0f;
    public Color firstCube;
    public Color secoundCube;


    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
{
    for (int x = 0; x < gridWidth; x++)
    {
        for (int y = 0; y < gridHeight; y++)
        {
            GameObject tile = Instantiate(tilePrefab, new Vector3(x * tileSize, 0, y * tileSize), Quaternion.identity);
            TileInfo tileComponent = tile.AddComponent<TileInfo>();
            tileComponent.SetTileInfo(x, y);
            tile.name = $"Tile_{x}_{y}";
            tile.transform.parent = transform;
            
           // tileComponent.SetColor(normalColor);


             // Set color based on checkerboard pattern
                if ((x + y) % 2 == 0)
                {
                    tileComponent.SetColor(firstCube); // White for even sums of x + y
                }
                else
                {
                    tileComponent.SetColor(secoundCube); // Black for odd sums of x + y
                }

           
        }
    }
}

}


// void GenerateGrid()
//     {
//         for (int x = 0; x < gridSize; x++)
//         {
//             for (int y = 0; y < gridSize; y++)
//             {
//                 Vector3 position = new Vector3(x, 0, y);
//                 GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
//                 cube.name = $"Cube_{x}_{y}";
//                 cube.AddComponent<TileInfo>().SetTileInfo(x, y);
//             }
//         }
//     }

