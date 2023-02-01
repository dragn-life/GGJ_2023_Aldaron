namespace _Scripts.States.Player
{
  public class PlayerStartState : BasePlayerState
  {
    public override void EnterState(PlayerController player)
    {
      player.SwitchState(player.IdleState);
    }

    public override void OnUpdate(PlayerController player)
    {
      // no-op
    }
  }
}