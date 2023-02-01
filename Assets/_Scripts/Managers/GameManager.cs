using System;
using _Scripts.States;
using UnityEngine;

namespace _Scripts.Managers
{
  public class GameManager : MonoBehaviour
  {
    public GameStartState StartState { get; } = new GameStartState();
    public GamePlayState PlayState { get; } = new GamePlayState();
    public GameVictoryState GameVictoryState { get; } = new GameVictoryState();
    public GameOverState GameOverState { get; } = new GameOverState();

    public event Action StartGameEvent;
    public event Action VictoryEvent;
    public event Action GameOverEvent;

    private BaseGameState _currentState;

    private void OnEnable()
    {
      SwitchState(StartState);
    }

    public void SwitchState(BaseGameState newState)
    {
      _currentState = newState;
      _currentState.EnterState(this);
    }

    public void WinGame()
    {
      SwitchState(GameVictoryState);
    }

    public void LoseGame()
    {
      SwitchState(GameOverState);
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