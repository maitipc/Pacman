using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameDatabase")]
public class GameDatabase : ScriptableObject {
    [Header("Game General Data")]
    [Tooltip("Pacman initial lives.")]
    public int initialLives = 4;
    [Tooltip("Ghosts' vulnerable state duration, caused by eat a power pellet.")]
    public int powerPelletEffectDuration = 8;
}
