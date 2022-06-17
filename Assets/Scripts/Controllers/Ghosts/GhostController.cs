using UnityEngine;

public class GhostController
{
    IGhostModel model;
    GhostView view;
    GhostsDatabase database;

    public GhostController(GhostView view, IGhostModel model)
    {
        this.view = view;
        this.model = model;

        database = view.Database;

        model.OnChangeState += HandleChangeState;
        view.OnPacmanCollision += HandleGhostCollision;
        view.OnGhostRespawned += HandleGhostOutHome;
        view.OnScatterStateEnd += HandleOnScatterEnd;
    }

    public void Initialize(PlayerView playerView, Transform insideHome, Transform outsideHome)
    {
        view.Target = playerView.gameObject;
        view.AtHome = insideHome;
        view.OutHome = outsideHome;
        model.Points = database.Points;

        model.CurrentState = GhostState.Chase;
        view.CurrentState = GhostState.Chase;

        view.gameObject.SetActive(true);
    }

    void HandleGhostCollision () => model.GhostCollide();
    void HandleGhostOutHome () => model.ChangeState(GhostState.Chase);
    void HandleOnScatterEnd () => model.ChangeState(GhostState.Chase);
    void HandleChangeState (GhostState state) => view.ChangeState(state);
}
