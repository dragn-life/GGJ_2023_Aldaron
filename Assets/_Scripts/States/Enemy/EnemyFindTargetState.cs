namespace _Scripts.States.Enemy
{
  public class EnemyFindTargetState : BaseEnemyState
  {
    public override void EnterState(EnemyController enemy)
    {
    }

    public override void OnUpdate(EnemyController enemy)
    {
    }
    
    public override void OnLateUpdate(EnemyController enemy)
    {
      enemy.GotoDestination();
    }
  }
}