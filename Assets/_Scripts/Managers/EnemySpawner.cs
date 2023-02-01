using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private GameManager gameManager;
  [SerializeField] private int initialSpawnsPerTarget = 2;
  [SerializeField] private float spawnInterval = 5.0f;

  [SerializeField] private List<EnemyController> enemyPrefabs;
  [SerializeField] private List<Transform> targetDestinations;
  [SerializeField] private List<Transform> spawnLocations;

  private bool _isSpawning;
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
      for (int i = 0; i < initialSpawnsPerTarget; i++)
      {
        SpawnEnemy(target);
      }
    }

    Invoke(nameof(SpawnNextEnemy), spawnInterval);
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
      Invoke(nameof(SpawnNextEnemy), spawnInterval);
    }
  }

  private void SpawnEnemy(Transform target)
  {
    EnemyController selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
    EnemyController unit = Instantiate(selectedEnemy, spawnLocation);
    unit.GameManager = gameManager;
    unit.SetNextDestination(target);
  }
}