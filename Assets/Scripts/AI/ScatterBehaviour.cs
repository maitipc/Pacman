using System.Collections.Generic;
using UnityEngine;

public class ScatterBehaviour 
{
    public Vector2 GenerateDirection(List<Vector2> availableDirections)
    {
        int index = Random.Range(0, availableDirections.Count);
        return availableDirections[index];
    }
}