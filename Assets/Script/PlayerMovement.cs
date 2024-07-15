using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Pathfinding pathfinding; // Reference to the Pathfinding script
    public float speed = 5f; // Movement speed of the player
    public Text positionText; // UI Text element to display player's position
    private List<Node> path; // List to hold the path found by the pathfinding algorithm
    private int targetIndex; // Index to track the current target node in the path
    private bool isMoving; // Flag to check if the player is currently moving
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Get the Animator component attached to the player GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the left mouse button is pressed and the player is not moving
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Create a ray from the main camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform a raycast and check if it hits any object
            if (Physics.Raycast(ray, out hit))
            {
                // Get the target position from the hit point
                Vector3 targetPosition = hit.point;

                // Find the path from the current position to the target position
                path = pathfinding.FindPath(transform.position, targetPosition);

                // If a path is found, start the movement coroutine
                if (path != null)
                {
                    StopCoroutine("FollowPath");
                    StartCoroutine("FollowPath");
                }
            }
        }

        // Update the UI text with the current target index in the path
        if (path != null && targetIndex < path.Count)
        {
            positionText.text = $"Position: {targetIndex}";
        }
    }

    // Coroutine to move the player along the path
    IEnumerator FollowPath()
    {
        isMoving = true; // Set the moving flag to true
        animator.SetBool("isWalking", true); // Start the walking animation

        // Get the first waypoint in the path
        Vector3 currentWaypoint = path[0].Position;

        while (true)
        {
            // Check if the player has reached the current waypoint
            if (transform.position == currentWaypoint)
            {
                // Move to the next waypoint
                targetIndex++;
                // Check if the player has reached the end of the path
                if (targetIndex >= path.Count)
                {
                    isMoving = false; // Set the moving flag to false
                    animator.SetBool("isWalking", false); // Stop the walking animation
                    yield break; // Exit the coroutine
                }
                // Update the current waypoint to the next position in the path
                currentWaypoint = path[targetIndex].Position;
            }

            // Move the player towards the current waypoint
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            yield return null; // Wait for the next frame
        }
    }
}
