using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyManager", menuName = "ScriptableObjects/DifficultyManager")]
public class DifficultyManagerSO : ScriptableObject
{
  [SerializeField] private List<DifficultyLevelSO> difficulties;

  [SerializeField] private int currentDifficultyIndex;

  private void Awake()
  {
    ResetDifficulty();
  }

  public DifficultyLevelSO CurrentDifficulty()
  {
    return difficulties[currentDifficultyIndex];
  }

  public void ResetDifficulty()
  {
    currentDifficultyIndex = 0;
  }

  public bool IsOnLastDifficulty()
  {
    return currentDifficultyIndex == difficulties.Count - 1;
  }

  public int CurrentDifficultyLevel()
  {
    return currentDifficultyIndex + 1;
  }

  public DifficultyLevelSO GotoNextDifficulty()
  {
    currentDifficultyIndex++;
    if (currentDifficultyIndex >= difficulties.Count)
    {
      currentDifficultyIndex = 0;
    }

    return difficulties[currentDifficultyIndex];
  }
}