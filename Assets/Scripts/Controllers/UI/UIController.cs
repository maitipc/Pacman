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
        if (lives <= 0)
            HandleGameOver();
        
        view.SetLives(lives);
    }

    void HandleGameOver() => view.GameOverBanner.SetActive(true);

    public void HandleWinner() => view.WinnerBanner.SetActive(true);
}