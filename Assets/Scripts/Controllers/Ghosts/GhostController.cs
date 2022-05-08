using UnityEngine;

public class GhostController
{
    const float MOVEMENT_SPEED = 5f;
    const float SPEED_MULTIPLIER = 1.2f;

    IGhostModel model;
    GhostView view;

    public GhostController(GhostView view, IGhostModel model)
    {
        this.view = view;
        this.model = model;

        model.OnChangeState += HandleChangeState;
        view.OnPacmanCollision += HandleGhostCollision;
        view.OnGhostRespawned += HandleGhostOutHome;
        view.OnScatterStateEnd += HandleOnScatterEnd;
    }

    public void Initialize(PlayerView playerView, Transform insideHome, Transform outsideHome)
    {
        view.Speed = MOVEMENT_SPEED;
        view.Multiplier = SPEED_MULTIPLIER;
        view.Target = playerView.gameObject;
        view.AtHome = insideHome;
        view.OutHome = outsideHome;
        model.GhostName = view.Name;

        model.CurrentState = GhostState.Chase;
        view.CurrentState = GhostState.Chase;

        view.gameObject.SetActive(true);
    }

    void HandleGhostCollision () => model.GhostCollide();
    void HandleGhostOutHome () => model.ChangeState(GhostState.Chase);
    void HandleOnScatterEnd () => model.ChangeState(GhostState.Chase);
    void HandleChangeState (GhostState state) => view.ChangeState(state);
}
