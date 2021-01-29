using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    UIController uiController;
    [SerializeField]
    AudioController audioController;
    [SerializeField]
    Transform melonsGameObject;
    [SerializeField]
    string nextLevel;
    public int maxLevelScore;
    int score;
    bool isPaused;
    public int Score { get { return score; } set { } }
    public Player Player { get { return player; } set { } }
    public UIController UIController { get { return uiController; } set { } }
    public AudioController AudioController { get { return audioController; } set { } }
    public static GameController Instance { get; set; }

    void Start()
    {
        Time.timeScale = 1;
        maxLevelScore = melonsGameObject.childCount;

        isPaused = false;
        Instance = this;
        player.Initialize();
        uiController.Initialize();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Restart"))
            ReloadLevel();

        ListenForPause();
            

        if (score >= maxLevelScore)
        {
            LevelFinished();
        }
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1;        
        SceneManager.LoadScene(nextLevel);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    void LevelFinished()
    {
        Time.timeScale = 0;
        GameController.Instance.AudioController.PlaySound(SoundType.LevelFinish);
        uiController.LevelClear();
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
    }

    void ListenForPause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 1;
                uiController.PauseMenu(isPaused);
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                uiController.PauseMenu(isPaused);
            }
        }
    }


}
