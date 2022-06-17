using System;

public class PlayerModel : IPlayerModel
{
    public event Action OnPlayerEaten;
    public event Action<int> OnIncreaseScore;

    public int Lives {get; set;}
    public int Score {get; private set;}

    public PlayerModel ()
    {
        Score = 0;
    }

    public void IncreaseScore (int score) 
    {
        Score += score;
        OnIncreaseScore?.Invoke(Score);
    }

    void DecreaseLife () => Lives--;

    public void PlayerEaten ()
    {
        DecreaseLife();
        OnPlayerEaten?.Invoke();
    }
}
