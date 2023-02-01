using _Scripts.Managers;

namespace _Scripts.States
{
  public class GameOverState : BaseGameState
  {
    public override void EnterState(GameManager game)
    {
      game.TriggerGameOverEvent();
    }
  }
}