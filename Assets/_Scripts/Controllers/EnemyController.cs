using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.States.Enemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour, IDamageable
{
  private static int MAX_HEALTH = 100;

  public EnemyStartState StartState { get; } = new EnemyStartState();
  public EnemyFindTargetState FindTargetState { get; } = new EnemyFindTargetState();
  public EnemyEatTreeState EatTreeState { get; } = new EnemyEatTreeState();
  public EnemyDamageState DamageState { get; } = new EnemyDamageState();
  public EnemyDeathState DeathState { get; } = new EnemyDeathState();

  [SerializeField] private Transform destination;

  [FormerlySerializedAs("attackDelay")] [SerializeField]
  private float attackInterval = 5.0f;

  [SerializeField] private int attackStrength = 2;

  public IDamageable damageableTarget;

  // public int Health { get; private set; }
  public int Health;
  public Animator Animator { get; private set; }

  private NavMeshAgent _navMeshAgent;

  private BaseEnemyState _currentState;
  private BaseEnemyState _lastState;

  private void OnEnable()
  {
    Animator = GetComponent<Animator>();
    _navMeshAgent = GetComponent<NavMeshAgent>();
    SwitchState(StartState);
  }

  private void Update()
  {
    _currentState.OnUpdate(this);
  }

  private void LateUpdate()
  {
    _currentState.OnLateUpdate(this);
  }

  private void OnCollisionEnter(Collision collision)
  {
    _currentState.OnCollisionEnter(collision, this);
  }

  private void OnCollisionExit(Collision collision)
  {
    DetectTreeExit(collision);
  }

  public void SwitchState(BaseEnemyState newState)
  {
    _lastState = _currentState;
    _currentState = newState;
    _currentState.EnterState(this);
  }

  public void SwitchState(BaseEnemyState newState, float delay)
  {
    StartCoroutine(SwitchStateCoroutine(newState, delay));
  }

  private IEnumerator SwitchStateCoroutine(BaseEnemyState newState, float delay)
  {
    yield return new WaitForSeconds(delay);
    SwitchState(newState);
  }

  public void SwitchToLastState()
  {
    if (_lastState == null)
    {
      Debug.Log("Last State was Null");
      return;
    }

    _currentState = _lastState;
    _currentState.EnterState(this);
  }

  public void SwitchToLastState(float delay)
  {
    StartCoroutine(SwitchToLastStateCoroutine(delay));
  }

  private IEnumerator SwitchToLastStateCoroutine(float delay)
  {
    yield return new WaitForSeconds(delay);
    SwitchToLastState();
  }

  public void ResetEnemy()
  {
    Health = MAX_HEALTH;
  }

  public void DestroySelf(float delay)
  {
    Destroy(gameObject, delay);
  }

  public void GotoDestination()
  {
    _navMeshAgent.destination = destination.position;
  }

  public bool DetectTree(Collision collision)
  {
    IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
    if (damageable != null)
    {
      damageableTarget = damageable;
      return true;
    }

    return false;
  }

  private bool DetectTreeExit(Collision collision)
  {
    IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
    if (damageable != null)
    {
      damageableTarget = null;
      return true;
    }

    return false;
  }

  public void EatTree()
  {
    damageableTarget.TakeDamage(attackStrength);
  }

  public void TakeDamage(int amount)
  {
    Health -= amount;
    if (Health > 0)
    {
      SwitchState(DamageState);
    }
    else
    {
      SwitchState(DeathState);
    }
  }
}