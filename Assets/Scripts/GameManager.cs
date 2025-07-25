using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    private int _score = 0;

    private ScoreUI _scoreUI;

    private int _ballCount = 1;

    private GameState _currentState = GameState.Playing;
    public GameState CurrentState => _currentState;

    public static event Action<bool> OnPauseChanged;
    public static event Action OnGameOver;

    public static event Action OnVictory;

    public static event Action<int> OnScoreUpdate;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic("gameBGM", .05f);
    }

    void Update()
    {
        //HandlePauseResumeInput();
    }

    void HandlePauseResumeInput()
    {
        if (_currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentState == GameState.GameOver)
                return;
            if (_currentState == GameState.Paused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void GameOver()
    {
        if (_currentState != GameState.Playing) return;
        _currentState = GameState.GameOver;
        Time.timeScale = 0f;
        AudioManager.Instance.PlaySFX("gameover", .25f);
        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        if (_currentState != GameState.GameOver) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        _currentState = GameState.Playing;
    }

    public void PauseGame()
    {
        if (_currentState != GameState.Playing) return;
        _currentState = GameState.Paused;
        Time.timeScale = 0f;
        OnPauseChanged?.Invoke(true);
    }

    public void ResumeGame()
    {
        if (_currentState != GameState.Paused) return;
        _currentState = GameState.Playing;
        Time.timeScale = 1f;
        OnPauseChanged?.Invoke(false);
    }

    public void UpdateScore(int value)
    {
        _score += value;
        OnScoreUpdate?.Invoke(_score);
    }

    void Victory()
    {
        AudioManager.Instance.PlaySFX("victory", .25f);
        OnVictory?.Invoke();
    }

    public int GetScore()
    {
        return _score;
    }

    public void RegisterBall()
    {
        _ballCount++;
    }

    public void UnregisterBall()
    {
        _ballCount--;

        if (_ballCount == 0)
        {
            Victory();
        }
    }
}