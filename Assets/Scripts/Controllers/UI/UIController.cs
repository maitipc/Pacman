using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController
{   
    IPlayerModel playerModel;
    UIView view;

    public void Setup (
        UIView view,
        IPlayerModel playerModel
    )
    {
        this.view = view;
        this.playerModel = playerModel;
            
        playerModel.OnPlayerEaten += HandlePlayerEaten;
        playerModel.OnIncreaseScore += UpdateScore;

        view.InitLives(playerModel.Lives);
        UpdateScore(playerModel.Score);
    }

    void UpdateScore(int score) => view.SetScore(score);

    void HandlePlayerEaten()
    {
        view.SetLives(playerModel.Lives);

        if (playerModel.Lives <= 0)
            HandleGameOver();
    }

    void HandleGameOver() => view.GameOver.SetActive(true);

    public void HandleWinner() => view.Winner.SetActive(true);
}