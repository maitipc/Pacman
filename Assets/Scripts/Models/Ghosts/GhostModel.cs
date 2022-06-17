using System;

public class GhostModel : IGhostModel
{
    public event Action<int> OnGhostEaten;
    public event Action OnPlayerEaten;
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
        CheckState();
    }

    void CheckState()
    {
        switch (CurrentState)
        {
            case GhostState.Scatter:
                OnPlayerEaten?.Invoke();
                break;
            case GhostState.Dead:
                break;
            case GhostState.Vulnerable:
                GhostEaten();
                break;
            case GhostState.Chase:
                OnPlayerEaten?.Invoke();
                ChangeState(GhostState.Scatter);
                break;
        }
    }

    public void ChangeState(GhostState state)
    {
        CurrentState = state;
        OnChangeState?.Invoke(state);
    }
}
