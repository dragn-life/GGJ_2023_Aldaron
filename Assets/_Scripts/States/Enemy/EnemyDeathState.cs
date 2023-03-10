using UnityEngine;

namespace _Scripts.States.Enemy
{
  public class EnemyDeathState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
      float deathTime = 5.0f;
      // Debug.Log("Enter Death State");
      enemy.navMeshAgent.speed = 0.0f;
      enemy.Animator.SetBool("Walk Forward", false);
      enemy.Animator.SetTrigger("Die");
      enemy.FreezeAnimation(1.3f);
      enemy.PlayDeathEffects(deathTime);
      enemy.DestroySelf(deathTime);
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