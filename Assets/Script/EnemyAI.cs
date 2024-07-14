using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, AI
{
    public float speed = 3f;
    public Transform player;
    private List<Node> path;
    private int targetIndex;
    private bool isMoving;

    private Pathfinding pathfinding;

    void Start()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector3 targetPosition = GetTargetPositionNearPlayer();
            path = pathfinding.FindPath(transform.position, targetPosition);
            if (path != null && path.Count > 0)
            {
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
    }

    Vector3 GetTargetPositionNearPlayer()
    {
        Node playerNode = pathfinding.GetNodeFromWorldPosition(player.position);
        List<Node> neighbors = pathfinding.GetNeighbors(playerNode);

        foreach (Node neighbor in neighbors)
        {
            if (neighbor.IsWalkable)
            {
                return neighbor.Position;
            }
        }

        return player.position; // fallback to player's position
    }

    IEnumerator FollowPath()
    {
        isMoving = true;
        Vector3 currentWaypoint = path[0].Position;

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Count)
                {
                    isMoving = false;
                    yield break;
                }
                currentWaypoint = path[targetIndex].Position;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void MoveTowardsTarget(Vector3 targetPosition)
    {
        path = pathfinding.FindPath(transform.position, targetPosition);
        if (path != null && path.Count > 0)
        {
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
}
