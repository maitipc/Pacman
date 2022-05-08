using System;

public class PlayerModel : IPlayerModel
{
    const int INITIAL_LIVES = 4;

    public event Action OnPlayerEaten;
    public event Action<int> OnIncreaseScore;

    public int Lives {get; private set;}
    public int Score {get; private set;}

    public PlayerModel ()
    {
        Initialize();
    }

    public void Initialize()
    {
        Lives = INITIAL_LIVES;
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
