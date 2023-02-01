namespace _Scripts.States.Enemy
{
  public class EnemyStartState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
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
  }
}