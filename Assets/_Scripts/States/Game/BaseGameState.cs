using _Scripts.Managers;

namespace _Scripts.States
{
  public abstract class BaseGameState
  {
    public abstract void EnterState(GameManager game);
  }
}