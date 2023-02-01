using _Scripts.Managers;

namespace _Scripts.States
{
  public class GamePlayState : BaseGameState
  {
    public override void EnterState(GameManager game)
    {
      game.SwitchState(game.PlayState);
    }
  }
}