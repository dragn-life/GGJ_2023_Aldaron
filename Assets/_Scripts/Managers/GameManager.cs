using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.States;
using UnityEngine;

namespace _Scripts.Managers
{
  public class GameManager : MonoBehaviour
  {
    public GameReadyState ReadyState { get; } = new GameReadyState();
    public GameStartState StartState { get; } = new GameStartState();
    public GamePlayState PlayState { get; } = new GamePlayState();
    public GameVictoryState GameVictoryState { get; } = new GameVictoryState();
    public GameOverState GameOverState { get; } = new GameOverState();

    [SerializeField] private DifficultyManagerSO difficultyManager;
    [SerializeField] private float startGameDelay = 1.0f;
    public event Action StartGameEvent;
    public event Action VictoryEvent;
    public event Action GameOverEvent;

    private BaseGameState _currentState;

    private void OnEnable()
    {
      SwitchState(ReadyState);
    }

    public void SwitchState(BaseGameState newState)
    {
      _currentState = newState;
      _currentState.EnterState(this);
    }

    public void SwitchState(BaseGameState newState, float delay)
    {
      StartCoroutine(SwitchStateCoroutine(newState, delay));
    }

    public void StartGame()
    {
      Analytics.TrackEvent("startGame", new Dictionary<string, object>()
      {
        { "level", difficultyManager.CurrentDifficultyLevel().ToString() }
      });
      SwitchState(StartState, startGameDelay);
    }

    private IEnumerator SwitchStateCoroutine(BaseGameState newState, float delay)
    {
      yield return new WaitForSeconds(delay);
      SwitchState(newState);
    }

    public void WinGame()
    {
      if (_currentState == PlayState)
      {
        Analytics.TrackEvent("wonGame", new Dictionary<string, object>()
        {
          { "level", difficultyManager.CurrentDifficultyLevel().ToString() }
        });
        SwitchState(GameVictoryState);
      }
    }

    public void LoseGame()
    {
      if (_currentState == PlayState)
      {
        Analytics.TrackEvent("lostGame", new Dictionary<string, object>()
        {
          { "level", difficultyManager.CurrentDifficultyLevel().ToString() }
        });
        SwitchState(GameOverState);
      }
    }

    public void TriggerStartGameEvent()
    {
      StartGameEvent?.Invoke();
    }

    public void TriggerVictoryGameEvent()
    {
      VictoryEvent?.Invoke();
    }

    public void TriggerGameOverEvent()
    {
      GameOverEvent?.Invoke();
    }
  }
}