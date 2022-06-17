using System;

public interface IPlayerModel
{
    event Action OnPlayerEaten;
    event Action<int> OnIncreaseScore;

    int Lives {get; set;}
    int Score {get;}

    void IncreaseScore (int score);
    void PlayerEaten ();
}