using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIView : MonoBehaviour
{
    const string LIVES_PATH = "Prefabs/Life";

    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject winner;
    [SerializeField] Transform livesArea;

    public GameObject GameOver => gameOver;
    public GameObject Winner => winner;
    public Transform LivesArea => livesArea;

    public void SetScore(int score) => scoreTxt.text = score.ToString();

    GameObject[] livesObjects;

    public void InitLives (int lives)
    {
        Object lifePrefab = Resources.Load(LIVES_PATH);
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

    void Update()
    {
        if (gameOver.gameObject.activeSelf || winner.gameObject.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.UnloadSceneAsync(gameObject.scene);
                SceneManager.LoadSceneAsync("Main");
            }     
        }
    }
}
