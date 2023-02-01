using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemyDeathState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      enemy.Animator.SetBool("Walk Forward", false);
      enemy.Animator.SetTrigger("Die");
      enemy.Animator.StopPlayback();
      enemy.DestroySelf(1.4f);
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