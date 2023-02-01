using UnityEngine;

public class PlayerShootState : BasePlayerState
{
  public override void EnterState(PlayerController player)
  {
    player.ResetAnimations();
    player.Animator.SetTrigger("Shoot");

    player.Invoke(nameof(PlayerController.ShootArrow), player.arrowShootDelay);
  }

  public override void OnUpdate(PlayerController player)
  {
    player.HandleAim();
  }
}