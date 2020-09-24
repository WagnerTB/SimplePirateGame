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
    public static GameStatus onEnd;

    public delegate void GameEvents();
    public static GameEvents onEnemyDie;

    public float startDelay = 1;
    public float gameElapsedTime = 0;

    [SerializeField] private float _gameSessionTime = 30;
    [SerializeField] private float _enemySpawnTime = 1;

    public static float gameSessionTime { get; private set; }
    public static float enemySpawnTime { get; private set; }

    public SpawnManager spawnManager;


    private void Start()
    {
        SceneManager.activeSceneChanged += SceneChanged;
        Initialize();
    }

    private void SceneChanged(Scene lastScene, Scene currentScene)
    {
        Debug.Log("Current Scene " + currentScene.name);
        if(currentScene.name == "GameScene")
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        _gameSessionTime = gameSessionTime;
        _enemySpawnTime = enemySpawnTime;
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
            onEnd?.Invoke();
        }
    }

    public static void SetGameSession(float gameSession)
    {
        float minGameSessionTime = 60;
        float maxGameSessionTime = 180;
        float clampedTime = Mathf.Clamp(gameSession, minGameSessionTime, maxGameSessionTime);
        Debug.Log("From " + gameSessionTime + " To" + clampedTime);
        gameSessionTime = clampedTime;
    }

    public static void SetEnemySpawnTime(float time)
    {
        float minEnemySpawnTime = 1;
        float maxEnemySpawnTime = 99;

        float clampedSpawnTime = Mathf.Clamp(time, minEnemySpawnTime, maxEnemySpawnTime);
        Debug.Log("From " + enemySpawnTime + " To" + clampedSpawnTime);

        enemySpawnTime = clampedSpawnTime;
    }
}
