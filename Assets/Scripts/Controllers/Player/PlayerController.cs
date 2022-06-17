public class PlayerController
{
    PlayerView view;
    IPlayerModel model;
    IGhostModel[] ghostModels;
    PacmanDatabase database;

    public PlayerController (PlayerView view, IPlayerModel model, IGhostModel[] ghostModels)
    {
        this.view = view;
        this.model = model;
        this.ghostModels = ghostModels;

        database = view.Database;
        model.OnPlayerEaten += HandlePlayerEaten;

        foreach (IGhostModel ghostModel in ghostModels)
        {
            ghostModel.OnGhostEaten += HandleGhostEaten;
            ghostModel.OnGhostCollision += HandleGhostCollision;
        }

        Initialize();
    }

    public void Initialize()
    {
        model.Lives = database.initialLives;
        
        ShowPlayer(true);
    }

    void HandleGhostCollision(IGhostModel ghostModel)
    {
        if (ghostModel.CurrentState == GhostState.Vulnerable)
            ghostModel.GhostEaten();          
        else if (ghostModel.CurrentState == GhostState.Dead)
            return;
        else
            model.PlayerEaten();
    }

    void HandlePlayerEaten()
    {
        foreach (IGhostModel ghostModel in ghostModels)
            ghostModel.ChangeState(GhostState.Scatter);
            
        view.Reset();
        ShowPlayer(false);
    }

    void HandleGhostEaten(int score) => model.IncreaseScore(score);

    void ShowPlayer(bool show) => view.gameObject.SetActive(show);
}
