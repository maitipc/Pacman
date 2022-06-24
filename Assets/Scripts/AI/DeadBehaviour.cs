using System.Collections.Generic;
using UnityEngine;

public class DeadBehaviour
{
    public float Multiplier = 4.5f;

    List<Vector2> availableDirections { get; set; }
    Transform target { get; set; }
    GameObject ghost { get; set; }
    Vector2 direction { get; set; }
    float minDistance { get; set; }

    public Vector2 GoToHome (GameObject ghost, Transform target, List<Vector2> availableDirections)
    {
        this.availableDirections = availableDirections;
        this.ghost = ghost;
        this.target = target;

        direction = Vector2.zero;
        minDistance = float.MaxValue;

        foreach (Vector2 availableDirection in availableDirections)
        {
            Vector3 newPosition = ghost.transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (target.position - newPosition).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                direction = availableDirection;
            }
        }

        return direction;
    }
}