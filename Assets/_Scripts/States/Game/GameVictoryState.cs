using _Scripts.Managers;

namespace _Scripts.States
{
  public class GameVictoryState : BaseGameState
  {
    public override void EnterState(GameManager game)
    {
      game.TriggerVictoryGameEvent();
    }
  }
}