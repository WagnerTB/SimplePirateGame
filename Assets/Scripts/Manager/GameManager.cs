using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

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

    public float startDelay = 1;
    public float enemySpawnTime = 1;
    public float gameSessionTime = 30;
    public float gameElapsedTime = 0;

    public SpawnManager spawnManager;

    private void Start()
    {
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
        if (gameElapsedTime >= gameSessionTime) return;

        gameElapsedTime += Time.deltaTime;

        if (gameElapsedTime >= gameSessionTime)
        {
            onEnd?.Invoke();
        }
    }
}
