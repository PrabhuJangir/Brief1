using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab; // Prefab of the obstacle (a red sphere)

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        for (int i = 0; i < obstacleData.obstacleArray.Length; i++)
        {
            if (obstacleData.obstacleArray[i])
            {
                int x = i % 10;
                int y = i / 10;
                Vector3 position = new Vector3(x, 0.5f, y); // Adjust the position as needed
                Instantiate(obstaclePrefab, position, Quaternion.identity);
            }
        }
    }
}
