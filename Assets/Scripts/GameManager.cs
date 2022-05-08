using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerView player;
    [SerializeField] GhostView[] ghosts;
    [SerializeField] UIView uiView;
    [SerializeField] Transform pelletsArea;
    [SerializeField] Transform insideHomePosition;
    [SerializeField] Transform outsideHomePosition;

    PlayerModel playerModel;
    GhostModel[] ghostModels;
    PelletView[] pellets;
    UIController uiController;

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        InitializeGhosts();
        InitializePlayer();
        InitializePellets();

        uiController = new UIController();
        uiController.Setup(uiView, playerModel);

        playerModel.OnPlayerEaten += HandlePlayerEaten;
    }

    void Reset()
    {
        player.gameObject.SetActive(true);
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
            ghostModels[i].Initialize();
            GhostController controller = new GhostController(ghosts[i], ghostModels[i]);
            controller.Initialize(player, insideHomePosition, outsideHomePosition);
        }
    }

    void InitializePlayer()
    {
        playerModel = new PlayerModel();
        PlayerController controller = new PlayerController(player, playerModel, ghostModels);
    }

    void HandlePlayerEaten()
    {
        if (playerModel.Lives > 0)
            Invoke(nameof(Reset), 2f);
        else
            player.gameObject.SetActive(false);
    }

    void HandlePelletEaten(int points, bool isPowerPellet, int effectDuration)
    {
        playerModel.IncreaseScore(points);

        if (isPowerPellet)
        {
            ChangeGhostState(GhostState.Vulnerable);
            StartCoroutine(Countdown(effectDuration));
        }

        if (!IsRemainingPellets())
            ShowWinner();
    }

    IEnumerator Countdown(int effectDuration)
    {
        while (effectDuration > 0)
        {
            yield return new WaitForSeconds(1);
            effectDuration--;
        }

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
}
