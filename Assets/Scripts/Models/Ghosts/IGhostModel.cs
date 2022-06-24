using System;

public interface IGhostModel
{
    event Action<int> OnGhostEaten;
    event Action OnPlayerEaten;
    event Action<GhostState> OnChangeState;

    GhostState CurrentState { get; set; }
    int Points { get; set; }
    
    void GhostEaten ();
    void CheckState();
    void ChangeState(GhostState state);
}