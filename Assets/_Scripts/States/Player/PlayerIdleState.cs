using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BasePlayerState
{
  public override void EnterState(PlayerController player)
  {
    player.Animator.SetBool("run", false);
    player.Animator.SetBool("idle2", true);
  }

  public override void OnUpdate(PlayerController player)
  {
    player.MovePlayer();
    if (player.IsPlayerMoving)
    {
      player.SwitchState(player.MoveState);
    }
  }
}