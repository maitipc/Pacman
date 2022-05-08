using System;

public interface IGhostModel
{
    event Action<int> OnGhostEaten;
    event Action<IGhostModel> OnGhostCollision;
    event Action<GhostState> OnChangeState;

    GhostName GhostName { get; set; }
    GhostState CurrentState { get; set; }
    int Points { get; }
    
    void GhostEaten ();
    void GhostCollide();
    void ChangeState(GhostState state);
}