using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour
{
    const float DISTANCE_FROM_PACMAN = 6f;
    public float Multiplier = 1.2f;

    List<Vector2> availableDirections { get; set; }
    Transform target { get; set; }
    GameObject ghost { get; set; }
    Vector2 direction { get; set; }
    float minDistance { get; set; }

    public Vector2 Chase (GameObject ghost, Transform target, GhostName ghostName, List<Vector2> availableDirections)
    {
        this.availableDirections = availableDirections;
        this.ghost = ghost;
        this.target = target;

        direction = Vector2.zero;
        minDistance = float.MaxValue;

        if (ghostName == GhostName.Blinky)
            ChaseBlinky();
        else if (ghostName == GhostName.Pinky || ghostName == GhostName.Inky)
            ChasePinkyInky();
        else
            ChaseClyde();

        return direction;
    }

    Vector2 ChaseBlinky()
    {
        //Blinky will follow pacman directly
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

    Vector2 ChasePinkyInky()
    {
        //Pinky and Inky will follow DISTANCE_FROM_PACMAN ahead pacman
        foreach (Vector2 availableDirection in availableDirections)
        {
            Vector3 newPosition = ghost.transform.position + new Vector3(availableDirection.x, availableDirection.y);
            Vector3 targetAhead = target.position + target.forward * DISTANCE_FROM_PACMAN;
            float distance = (targetAhead - newPosition).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                direction = availableDirection;
            }
        }

        return direction;
    }

    Vector2 ChaseClyde()
    {
        //Clyde will follow DISTANCE_FROM_PACMAN above pacman
        foreach (Vector2 availableDirection in availableDirections)
        {
            Vector3 newPosition = ghost.transform.position + new Vector3(availableDirection.x, availableDirection.y);
            Vector3 targetAhead = target.position - target.forward * DISTANCE_FROM_PACMAN;
            float distance = (targetAhead - newPosition).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                direction = availableDirection;
            }
        }

        return direction;
    }
}