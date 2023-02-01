using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemyFindTargetState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      // Debug.Log("Enter Find Target State");
      enemy.Animator.SetBool("Walk Forward", true);

      // If target is already found, trigger eat
      if (enemy.DamageableTarget != null)
      {
        enemy.SwitchState(enemy.EatTreeState);
      }
    }

    public override void OnUpdate(EnemyController enemy)
    {
      //no-op
    }

    public override void OnLateUpdate(EnemyController enemy)
    {
      enemy.GotoDestination();
    }

    public override void OnCollisionEnter(Collision collision, EnemyController enemy)
    {
      if (enemy.DetectTree(collision))
      {
        enemy.SwitchState(enemy.EatTreeState);
      }
    }
  }
}