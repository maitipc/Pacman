using System;

public interface IGhostModel
{
    event Action<int> OnGhostEaten;
    event Action<IGhostModel> OnGhostCollision;
    event Action<GhostState> OnChangeState;

    GhostState CurrentState { get; set; }
    int Points { get; set; }
    
    void GhostEaten ();
    void GhostCollide();
    void ChangeState(GhostState state);
}