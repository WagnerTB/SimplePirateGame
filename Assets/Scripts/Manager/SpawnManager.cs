using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    #region Instance region || Awake
    public static SpawnManager Instance { get { return GetInstance(); } }
    private static SpawnManager _instance;

    private static SpawnManager GetInstance()
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

        RegisterEvents();

    }
    #endregion

    private float _enemySpawnTime;

    public SpawnPoint[] spawnPoints;
    public AIController[] enemies;

    private void OnDestroy()
    {
        UnRegisterEvents();
    }

    private void RegisterEvents()
    {
        GameManager.onEndGame += StopAllCoroutines;
    }

    private void UnRegisterEvents()
    {
        GameManager.onEndGame -= StopAllCoroutines;
    }

    public void BeginEnemySpawn(float spawnTime)
    {
        _enemySpawnTime = spawnTime;
        StartCoroutine(CoEnemySpawn(_enemySpawnTime));
    }

    private IEnumerator CoEnemySpawn(float time)
    {
        SpawnEnemy();
        yield return new WaitForSeconds(time);
        StartCoroutine((CoEnemySpawn(time)));
    }

    private void SpawnEnemy()
    {
        var rnd = UnityEngine.Random.Range(0, enemies.Length);
        var enemy = Instantiate(enemies[rnd]);
        enemy.transform.position = GetPoint().RequestPosition();
    }

    private SpawnPoint GetPoint()
    {
        var rnd = UnityEngine.Random.Range(0, spawnPoints.Length);
        return spawnPoints[rnd];
    }

}
