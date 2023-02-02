namespace _Scripts.States.Player
{
  public class PlayerPauseState : BasePlayerState
  {
    public override void EnterState(PlayerController player)
    {
      player.ResetAnimations();
      player.Animator.SetBool("idle2", true);
    }

    public override void OnUpdate(PlayerController player)
    {
      //no-op
    }

    public override void OnLateUpdate(PlayerController player)
    {
      //no-op
    }
  }
}