using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemyEatTreeState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      // Debug.Log("Enter Eat Tree State");
      if (enemy.ShouldBeDead)
      {
        enemy.SwitchState(enemy.DeathState);
        return;
      }

      enemy.Animator.SetBool("Walk Forward", false);
      enemy.Animator.SetTrigger("Punch");
      enemy.EatTree();
      enemy.SwitchState(enemy.FindTargetState, enemy.attackInterval);
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