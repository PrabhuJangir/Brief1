using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Node[,] grid;
    public Transform player;
    public ObstacleData obstacleData;
    public int gridSize = 10;
    public float nodeSize = 1f;

    private void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 worldPosition = new Vector3(x * nodeSize, 0, y * nodeSize);
                bool isWalkable = !obstacleData.obstacleArray[x + y * gridSize];
                grid[x, y] = new Node(worldPosition, isWalkable);
            }
        }
    }

    public List<Node> FindPath(Vector3 start, Vector3 end)
    {
        Node startNode = GetNodeFromWorldPosition(start);
        Node endNode = GetNodeFromWorldPosition(end);

        if (startNode == null || endNode == null) return null;

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.IsWalkable || closedSet.Contains(neighbor))
                    continue;

                float newMovementCostToNeighbor = currentNode.GCost + Vector3.Distance(currentNode.Position, neighbor.Position);
                if (newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = newMovementCostToNeighbor;
                    neighbor.HCost = Vector3.Distance(neighbor.Position, endNode.Position);
                    neighbor.Parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

  public Node GetNodeFromWorldPosition(Vector3 worldPosition)
{
    int x = Mathf.RoundToInt(worldPosition.x / nodeSize);
    int y = Mathf.RoundToInt(worldPosition.z / nodeSize);

    if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
    {
        return grid[x, y];
    }
    return null;
}

public List<Node> GetNeighbors(Node node)
{
    List<Node> neighbors = new List<Node>();
    int x = Mathf.RoundToInt(node.Position.x / nodeSize);
    int y = Mathf.RoundToInt(node.Position.z / nodeSize);

    for (int dx = -1; dx <= 1; dx++)
    {
        for (int dy = -1; dy <= 1; dy++)
        {
            if (dx == 0 && dy == 0) continue;

            int checkX = x + dx;
            int checkY = y + dy;

            if (checkX >= 0 && checkX < gridSize && checkY >= 0 && checkY < gridSize)
            {
                neighbors.Add(grid[checkX, checkY]);
            }
        }
    }

    return neighbors;
}


    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }
}
