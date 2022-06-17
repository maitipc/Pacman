using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController
{   
    UIView view;

    public void Setup (UIView view, int lives)
    {
        this.view = view;

        view.InitLives(lives);
    }

    public void UpdateScore(int score) => view.SetScore(score);

    public void HandlePlayerEaten(int lives)
    {
        view.SetLives(lives);

        if (lives <= 0)
            HandleGameOver();
    }

    void HandleGameOver() => view.GameOver.SetActive(true);

    public void HandleWinner() => view.Winner.SetActive(true);
}