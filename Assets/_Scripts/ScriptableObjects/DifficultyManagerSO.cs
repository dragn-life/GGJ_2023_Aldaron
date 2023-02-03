using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DifficultyManager", menuName = "ScriptableObjects/DifficultyManager")]
public class DifficultyManagerSO : ScriptableObject
{
  [SerializeField] private List<DifficultyLevelSO> difficulties;

  private int _currentDifficultyIndex;

  private void Awake()
  {
    ResetDifficulty();
  }

  public DifficultyLevelSO CurrentDifficulty()
  {
    return difficulties[_currentDifficultyIndex];
  }

  public void ResetDifficulty()
  {
    _currentDifficultyIndex = 0;
  }

  public bool IsOnLastDifficulty()
  {
    return _currentDifficultyIndex == difficulties.Count - 1;
  }

  public int CurrentDifficultyLevel()
  {
    return _currentDifficultyIndex + 1;
  }

  public DifficultyLevelSO GotoNextDifficulty()
  {
    _currentDifficultyIndex++;
    if (_currentDifficultyIndex >= difficulties.Count)
    {
      _currentDifficultyIndex = 0;
    }

    return difficulties[_currentDifficultyIndex];
  }
}