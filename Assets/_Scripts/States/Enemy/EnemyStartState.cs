using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemyStartState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      // Debug.Log("Enter Start State");
      enemy.ResetEnemy();
      enemy.SwitchState(enemy.FindTargetState);
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