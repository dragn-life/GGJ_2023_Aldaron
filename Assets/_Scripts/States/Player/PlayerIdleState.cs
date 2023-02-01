using UnityEngine;

public class PlayerIdleState : BasePlayerState
{
  public override void EnterState(PlayerController player)
  {
    player.ResetAnimations();
    player.Animator.SetBool("idle2", true);
  }

  public override void OnUpdate(PlayerController player)
  {
    if (player.MovePlayer())
    {
      player.SwitchState(player.MoveState);
    }

    if (player.CheckShooting())
    {
      player.SwitchState(player.ShootState);
    }

    player.HandleAim();
  }
}