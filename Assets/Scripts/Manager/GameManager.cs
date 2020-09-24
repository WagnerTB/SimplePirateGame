using System;
using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Instance region
    public static GameManager Instance { get { return GetInstance(); } }
    private static GameManager _instance;

    private static GameManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public enum GameState
    {
        PreBegin,
        Pause,
        Playing,
        End
    }

    public GameState currentState;

    public delegate void GameStatus();
    public static GameStatus onPause;
    public static GameStatus onEndGame;

    public delegate void GameEvents();
    public static GameEvents onEnemyDie;

    public float startDelay = 1;
    public float gameElapsedTime = 0;

    [SerializeField] private float _gameSessionTime = 30;
    [SerializeField] private float _enemySpawnTime = 1;

    public static float gameSessionTime { get; private set; }
    public static float enemySpawnTime { get; private set; }

    public SpawnManager spawnManager;
    public PlayerController player;

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneChanged;
        onPause += PauseGame;
        Initialize();
    }

    public void ContinueGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        currentState = GameState.Pause;
        Time.timeScale = 0;
    }

    public static void EndGame()
    {
        GameManager.Instance.currentState = GameState.End;
        Time.timeScale = 1;
        onEndGame?.Invoke();
    }

    private void SceneChanged(Scene lastScene, Scene currentScene)
    {
        if(currentScene.name == "GameScene")
        {
            Initialize();
        }
        else if(currentScene.name != "GameScene")
        {
            spawnManager.StopAllCoroutines();
            currentState = GameState.PreBegin;
        }
    }

    private void Initialize()
    {
        if(gameSessionTime > 0)
            _gameSessionTime = gameSessionTime;
        else
        {
            SetGameSession(_gameSessionTime);
        }

        if(enemySpawnTime > 0)
        _enemySpawnTime = enemySpawnTime;
        else
        {
            SetEnemySpawnTime(_enemySpawnTime);
        }

        if(player == null)
            player = FindObjectOfType<PlayerController>();

        gameElapsedTime = 0;

        Invoke(nameof(BeginGame), startDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == GameState.Playing)
            CheckGameSessionTime();
    }
    
    public void BeginGame()
    {
        spawnManager.BeginEnemySpawn(enemySpawnTime);
        currentState = GameState.Playing;
    }

    private void CheckGameSessionTime()
    {
        if (gameElapsedTime >= gameSessionTime || currentState == GameState.End) return;

        gameElapsedTime += Time.deltaTime;

        if (gameElapsedTime >= gameSessionTime)
        {
            EndGame();
        }
    }

    public static void SetGameSession(float gameSession)
    {
        float minGameSessionTime = 60;
        float maxGameSessionTime = 180;
        float clampedTime = Mathf.Clamp(gameSession, minGameSessionTime, maxGameSessionTime);

        gameSessionTime = clampedTime;
    }

    public static void SetEnemySpawnTime(float time)
    {
        float minEnemySpawnTime = 1;
        float maxEnemySpawnTime = 99;

        float clampedSpawnTime = Mathf.Clamp(time, minEnemySpawnTime, maxEnemySpawnTime);

        enemySpawnTime = clampedSpawnTime;
    }
}
