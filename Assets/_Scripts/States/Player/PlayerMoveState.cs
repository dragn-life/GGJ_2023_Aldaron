using UnityEngine;

public class PlayerMoveState : BasePlayerState
{
  public override void EnterState(PlayerController player)
  {
    player.Animator.SetBool("idle2", false);
    player.Animator.SetBool("run", true);
  }

  public override void OnUpdate(PlayerController player)
  {
    player.MovePlayer();
    if (!player.IsPlayerMoving)
    {
      player.SwitchState(player.IdleState);
    }
  }
}