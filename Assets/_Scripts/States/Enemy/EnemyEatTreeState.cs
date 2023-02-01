using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemyEatTreeState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      enemy.Animator.SetBool("Walk Forward", false);
      enemy.Animator.SetTrigger("Punch");
      enemy.EatTree();
      enemy.SwitchState(enemy.FindTargetState, 2.0f);
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
      // TODO: Check Arrows
    }
  }
}