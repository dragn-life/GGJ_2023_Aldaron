using _Scripts.Managers;

namespace _Scripts.States
{
  public class GameStartState : BaseGameState
  {
    public override void EnterState(GameManager game)
    {
      game.TriggerStartGameEvent();
      game.SwitchState(game.PlayState);
    }
  }
}