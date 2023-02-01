using UnityEngine;

namespace _Scripts.States.Enemy
{
  public abstract class BaseEnemyState
  {
    public abstract void EnterState(EnemyController enemy);
    public abstract void OnUpdate(EnemyController enemy);
    public abstract void OnLateUpdate(EnemyController enemy);
    public abstract void OnCollisionEnter(Collision collision, EnemyController enemy);
  }
}