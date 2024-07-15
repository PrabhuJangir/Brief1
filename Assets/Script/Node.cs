using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // Position of the node in the world
    public Vector3 Position { get; }

    // Indicates if the node is walkable (not blocked by an obstacle)
    public bool IsWalkable { get; }

    // Reference to the parent node (used to trace the path)
    public Node Parent { get; set; }

    // Cost from the start node to this node
    public float GCost { get; set; }

    // Estimated cost from this node to the end node
    public float HCost { get; set; }

    // Total cost (GCost + HCost)
    public float FCost => GCost + HCost;

    // Constructor to initialize the node with its position and walkability
    public Node(Vector3 position, bool isWalkable)
    {
        Position = position;
        IsWalkable = isWalkable;
    }
}
