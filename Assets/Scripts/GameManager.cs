using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerView player;
    [SerializeField] GhostView[] ghosts;
    [SerializeField] UIView uiView;
    [SerializeField] GameDatabase gameData;
    [SerializeField] Transform pelletsArea;
    [SerializeField] Transform insideHomePosition;
    [SerializeField] Transform outsideHomePosition;

    GhostModel[] ghostModels;
    PelletView[] pellets;
    UIController uiController;
    int score { get; set;}
    int lives {get; set;}

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        score = 0;
        lives = gameData.initialLives;

        InitializeGhosts();
        InitializePlayer();
        InitializePellets();

        uiController = new UIController();
        uiController.Setup(uiView, lives);

        foreach (IGhostModel ghostModel in ghostModels)
        {
            ghostModel.OnPlayerEaten += HandlePlayerEaten;
            ghostModel.OnGhostEaten += IncreaseScore;
        }
    }

    void InitializePellets()
    {
        pellets = pelletsArea.GetComponentsInChildren<PelletView>();

        foreach (PelletView pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
            pellet.OnPelletEaten += HandlePelletEaten;
        }
    }

    void InitializeGhosts()
    {
        ghostModels = new GhostModel[ghosts.Length];

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghostModels[i] = new GhostModel();
            GhostController controller = new GhostController(ghosts[i], ghostModels[i]);
            controller.Initialize(player, insideHomePosition, outsideHomePosition);
        }
    }

    void InitializePlayer() => player.gameObject.SetActive(true);

    void HandlePlayerEaten()
    {
        player.gameObject.SetActive(false);
        lives--;
        uiController.HandlePlayerEaten(lives);
        ChangeGhostState(GhostState.Scatter);

        if (lives > 0)
        {
            Invoke(nameof(InitializePlayer), 2f);
            player.Reset();
        }
    }

    public void IncreaseScore (int score) 
    {
        this.score += score;
        uiController.UpdateScore(this.score);
    }

    void HandlePelletEaten(int points, bool isPowerPellet)
    {
        IncreaseScore(points);

        if (isPowerPellet)
        {
            ChangeGhostState(GhostState.Vulnerable);
            StartCoroutine(Countdown(gameData.powerPelletEffectDuration));
        }

        if (!IsRemainingPellets())
            ShowWinner();
    }

    IEnumerator Countdown(int effectDuration)
    {
        yield return new WaitForSeconds(effectDuration);
        ChangeGhostState(GhostState.Chase);
    }

    void ChangeGhostState (GhostState state)
    {
         foreach (GhostModel ghost in ghostModels)
            ghost.ChangeState(state);
    }

    bool IsRemainingPellets()
    {
        foreach (PelletView pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
                return true;
        }

        return false;
    }

    void ShowWinner()
    {
        player.gameObject.SetActive(false);
        uiController.HandleWinner();
    }

    void DisposeGhosts()
    {
        foreach (IGhostModel ghostModel in ghostModels)
        {
            ghostModel.OnPlayerEaten -= HandlePlayerEaten;
            ghostModel.OnGhostEaten -= IncreaseScore;
        }
    }

    void DisposePellets()
    {
        foreach (PelletView pellet in pellets)
            pellet.OnPelletEaten -= HandlePelletEaten;
    }

    void OnDestroy() 
    {
        DisposeGhosts();
        DisposePellets();
    }
}
