using System;

public interface IPlayerModel
{
    event Action OnPlayerEaten;
    event Action<int> OnIncreaseScore;

    int Lives {get;}
    int Score {get;}

    void Initialize();
    void IncreaseScore (int score);
    void PlayerEaten ();
}