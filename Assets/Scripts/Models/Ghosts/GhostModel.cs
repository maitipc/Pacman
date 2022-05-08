using System;

public class GhostModel : IGhostModel
{
    const int POINTS = 200;

    public event Action<int> OnGhostEaten;
    public event Action<IGhostModel> OnGhostCollision;
    public event Action<GhostState> OnChangeState;

    public GhostName GhostName { get; set; }
    public GhostState CurrentState { get; set; }
    public int Points { get; private set; }
    int multiplier = 1;

    public void Initialize()
    {
        Points = POINTS;
    }

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
