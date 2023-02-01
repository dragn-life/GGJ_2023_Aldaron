using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemySpawnState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      float duration = 3f;
      enemy.Animator.SetBool("Walk Forward", false);
      enemy.PlaySpawnEffects(duration);
      enemy.SwitchState(enemy.FindTargetState, duration);
    }

    public override void OnUpdate(EnemyController enemy)
    {
      //no-op
    }

    public override void OnLateUpdate(EnemyController enemy)
    {
      //no-op
    }

    public override void OnCollisionEnter(Collision collision, EnemyController enemy)
    {
      //no-op
    }
  }
}