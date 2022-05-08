using System.Collections.Generic;
using UnityEngine;

public class VulnerableBehaviour 
{
    public float Multiplier = 0.5f;

    public Vector2 Vulnerable (GameObject ghost, Transform target, List<Vector2> availableDirections) 
    {
        Vector2 direction = Vector2.zero;
        float maxDistance = float.MinValue;

        foreach (Vector2 availableDirection in availableDirections)
        {
            Vector3 newPosition = ghost.transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (target.position - newPosition).sqrMagnitude;

            if (distance > maxDistance)
            {
                direction = availableDirection;
                maxDistance = distance;
            }
        }

        return direction;
    }
}