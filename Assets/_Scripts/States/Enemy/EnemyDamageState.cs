using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemyDamageState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      enemy.Animator.SetBool("Walk Forward", false);
      enemy.Animator.SetTrigger("Take Damage");
      enemy.SwitchState(enemy.FindTargetState, 1.0f);
    }

    public override void OnUpdate(EnemyController enemy)
    {
      // no-op
    }

    public override void OnLateUpdate(EnemyController enemy)
    {
      // no-op
    }

    public override void OnCollisionEnter(Collision collision, EnemyController enemy)
    {
      // no-op
    }
  }
}