using System;
using _Scripts.Managers;
using UnityEngine;

public class CountDownManager : MonoBehaviour
{
  [SerializeField] private GameManager gameManager;
  [SerializeField] private DifficultyManagerSO difficultyManager;

  public event Action<float> TimeChangeEvent;

  private bool _isCountingDown;
  private float _timeLeft;

  private void OnEnable()
  {
    gameManager.StartGameEvent += StartCountDown;
    gameManager.GameOverEvent += StopCountDown;
    gameManager.VictoryEvent += StopCountDown;
  }

  private void OnDisable()
  {
    gameManager.StartGameEvent -= StartCountDown;
    gameManager.GameOverEvent -= StopCountDown;
    gameManager.VictoryEvent -= StopCountDown;
  }

  // Update is called once per frame
  void Update()
  {
    if (_isCountingDown)
    {
      CountDown();
    }
  }

  private void CountDown()
  {
    _timeLeft -= Time.deltaTime;

    TimeChangeEvent?.Invoke(Mathf.Round(_timeLeft));

    if (_timeLeft < 0)
    {
      gameManager.WinGame();
    }
  }

  private void StopCountDown()
  {
    _isCountingDown = false;
  }

  private void StartCountDown()
  {
    _isCountingDown = true;
    _timeLeft = difficultyManager.CurrentDifficulty().TimeLimit;
  }
}