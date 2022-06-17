using System;

public class GhostModel : IGhostModel
{
    public event Action<int> OnGhostEaten;
    public event Action<IGhostModel> OnGhostCollision;
    public event Action<GhostState> OnChangeState;

    public GhostState CurrentState { get; set; }
    public int Points { get; set; }
    int multiplier = 1;

    public void GhostEaten ()
    {
        multiplier++;
        Points = Points * multiplier;
        OnGhostEaten?.Invoke(Points);
        ChangeState(GhostState.Dead);
    }

    public void GhostCollide()
    {
        OnGhostCollision?.Invoke(this);
    }

    public void ChangeState(GhostState state)
    {
        CurrentState = state;
        OnChangeState?.Invoke(state);
    }
}
