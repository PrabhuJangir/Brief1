using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Pathfinding pathfinding;
    public float speed = 5f;
    public Text positionText;
    private List<Node> path;
    private int targetIndex;
    private bool isMoving;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPosition = hit.point;
                path = pathfinding.FindPath(transform.position, targetPosition);
                if (path != null)
                {
                    StopCoroutine("FollowPath");
                    StartCoroutine("FollowPath");
                }
            }
        }

        if (path != null && targetIndex < path.Count)
        {
            positionText.text = $"Position: {targetIndex}";
        }
    }

    IEnumerator FollowPath()
    {
        isMoving = true;
        animator.SetBool("isWalking", true); // Start walking animation
        Vector3 currentWaypoint = path[0].Position;

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Count)
                {
                    isMoving = false;
                    animator.SetBool("isWalking", false); // Stop walking animation
                    yield break;
                }
                currentWaypoint = path[targetIndex].Position;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}
