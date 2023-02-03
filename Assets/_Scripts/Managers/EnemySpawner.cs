using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private GameManager gameManager;
  [SerializeField] private DifficultyManagerSO difficultyManager;

  [SerializeField] private List<EnemyController> enemyPrefabs;
  [SerializeField] private List<Transform> targetDestinations;
  [SerializeField] private List<Transform> spawnLocations;

  private bool _isSpawning = false;
  private int _nextTarget = 0;

  private void OnEnable()
  {
    gameManager.StartGameEvent += StartSpawning;
    gameManager.VictoryEvent += StopSpawning;
    gameManager.GameOverEvent += StopSpawning;
  }
  
  private void OnDisable()
  {
    gameManager.StartGameEvent -= StartSpawning;
    gameManager.VictoryEvent -= StopSpawning;
    gameManager.GameOverEvent -= StopSpawning;
  }

  private void StopSpawning()
  {
    _isSpawning = false;
  }

  private void StartSpawning()
  {
    _isSpawning = true;
    foreach (Transform target in targetDestinations)
    {
      for (int i = 0; i < difficultyManager.CurrentDifficulty().InitialEnemySpawnsPerTarget; i++)
      {
        SpawnEnemy(target);
      }
    }

    Invoke(nameof(SpawnNextEnemy), difficultyManager.CurrentDifficulty().EnemySpawnInterval);
  }

  private void SpawnNextEnemy()
  {
    SpawnEnemy(targetDestinations[_nextTarget]);
    if (_nextTarget >= targetDestinations.Count)
    {
      _nextTarget = 0;
    }

    if (_isSpawning)
    {
      Invoke(nameof(SpawnNextEnemy), difficultyManager.CurrentDifficulty().EnemySpawnInterval);
    }
  }

  private void SpawnEnemy(Transform target)
  {
    if (!_isSpawning)
    {
      return;
    }
    EnemyController selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
    EnemyController unit = Instantiate(selectedEnemy, spawnLocation);
    unit.GameManager = gameManager;
    unit.SubscribeToGameEvents();
    unit.SetNextDestination(target);
    unit.SwitchState(unit.StartState);
  }
}