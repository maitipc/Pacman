using System;
using System.Collections.Generic;
using UnityEngine;

public class TurningPoints : MonoBehaviour
{
    const float DISTANCE_FROM_TILE = 1f;

    public List<Vector2> availableDirections { get; private set; }

    LayerMask MazeWalls => LayerMask.GetMask("Walls");
    
    void Start ()
    {
        availableDirections = new List<Vector2>();

        CheckDirection(Vector2.down);
        CheckDirection(Vector2.up);
        CheckDirection(Vector2.left);
        CheckDirection(Vector2.right);
    }

    void CheckDirection(Vector2 direction)
    {
        RaycastHit2D raycast = Physics2D.Raycast(
            transform.position, 
            direction, 
            DISTANCE_FROM_TILE, 
            MazeWalls
        );

        if (raycast.collider == null)
            availableDirections.Add(direction);
    }
}