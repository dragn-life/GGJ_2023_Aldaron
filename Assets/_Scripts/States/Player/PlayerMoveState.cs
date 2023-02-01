using UnityEngine;

public class PlayerMoveState : BasePlayerState
{
  public override void EnterState(PlayerController player)
  {
    player.ResetAnimations();
    player.Animator.SetBool("run", true);
  }

  public override void OnUpdate(PlayerController player)
  {
    if (!player.MovePlayer())
    {
      player.SwitchState(player.IdleState);
    }

    if (player.CheckShooting())
    {
      player.SwitchState(player.ShootState);
    }
    player.HandleAim();
  }
}