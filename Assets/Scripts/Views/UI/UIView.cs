using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] GameObject gameOverBanner;
    [SerializeField] GameObject winnerBanner;
    [SerializeField] GameObject lifePrefab;
    [SerializeField] Transform livesArea;

    public GameObject GameOverBanner => gameOverBanner;
    public GameObject WinnerBanner => winnerBanner;

    public void SetScore(int score) => scoreTxt.text = score.ToString();

    GameObject[] livesObjects;

    public void InitLives (int lives)
    {
        livesObjects = new GameObject[lives];

        for (int i = 0; i < lives; i++)
            livesObjects[i] = Instantiate(lifePrefab, livesArea.transform) as GameObject;
    }

    public void SetLives (int lives)
    {
        for (int i = 0; i < livesObjects.Length; i++)
        {
            if (i < lives)
                livesObjects[i].gameObject.SetActive(true);
            else
                livesObjects[i].gameObject.SetActive(false);
        }
    }

    //colocar essa verificação de restart no game manager
    //a condição vem o game manager, o input vem da ui e o retorno vem do game manager
    void Update()
    {
        if (gameOverBanner.gameObject.activeSelf || winnerBanner.gameObject.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.UnloadSceneAsync(gameObject.scene);
                SceneManager.LoadSceneAsync("Main");
            }     
        }
    }
}
