using UnityEngine;

[CreateAssetMenu(fileName = "GhostsData", menuName = "ScriptableObjects/GhostsDatabase")]
public class GhostsDatabase : ScriptableObject
{
    [Header("Ghosts")]

    public GhostName Name;
    [Tooltip("Time for Ghost to respawn.")]
    public float atHomeDuration;
    [Tooltip("Points earned when a ghost is eaten.")]
    public int Points = 200;
    public float MovementSpeed = 5f;
    [Tooltip("Multiplier for ghost speed.")]
    public float SpeedMultiplier = 1.2f;
    [Tooltip("Duration of the scatter state. After, the state changes to Chase.")]
    public int ScatterDuration = 10;
}
