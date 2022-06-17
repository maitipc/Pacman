using UnityEngine;

[CreateAssetMenu(fileName = "PacmanData", menuName = "ScriptableObjects/PacmanDatabase")]
public class PacmanDatabase : ScriptableObject
{
    [Header("Pacman")]
    [Tooltip("Pacman initial lives.")]
    public int initialLives = 4;
    public float movementSpeed = 6f;
    [Tooltip("Multiplier for pacman speed.")]
    public float speedMultiplier = 1.2f; 
}
