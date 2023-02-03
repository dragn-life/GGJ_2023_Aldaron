using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemyNothingState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      //no-op
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