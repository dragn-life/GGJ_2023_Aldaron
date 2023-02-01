using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemyDeathState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      // Debug.Log("Enter Death State");
      enemy.Animator.SetBool("Walk Forward", false);
      enemy.Animator.SetTrigger("Die");
      enemy.FreezeAnimation(1.3f);
      enemy.PlayDeathEffects();
      enemy.DestroySelf(5f);
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