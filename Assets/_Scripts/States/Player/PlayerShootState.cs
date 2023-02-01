using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerShootState : BasePlayerState
{
  private float _shootDelay = 1.3f;
  private PlayerController _player;
  public override void EnterState(PlayerController player)
  {
    _player = player;
    player.ResetAnimations();
    player.Animator.SetTrigger("Shoot");

    player.Invoke(nameof(PlayerController.ShootArrow), _shootDelay);
  }

  public override void OnUpdate(PlayerController player)
  {
    //no-op
  }
}