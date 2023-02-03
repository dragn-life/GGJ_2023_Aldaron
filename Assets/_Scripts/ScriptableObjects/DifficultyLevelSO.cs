using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "ScriptableObjects/Difficulty Level")]
public class DifficultyLevelSO : ScriptableObject
{
  public float TimeLimit;

  public int InitialEnemySpawnsPerTarget;
  public int EnemySpawnInterval;
  public int EnemyHealth;
  public int EnemyAttackStrength;
  public float EnemyAttackInterval;
}