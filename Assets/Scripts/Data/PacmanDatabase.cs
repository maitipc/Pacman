using UnityEngine;

[CreateAssetMenu(fileName = "PacmanData", menuName = "ScriptableObjects/PacmanDatabase")]
public class PacmanDatabase : ScriptableObject
{
    [Header("Pacman")]
    public float movementSpeed = 6f;
    [Tooltip("Multiplier for pacman speed.")]
    public float speedMultiplier = 1.2f; 
}
