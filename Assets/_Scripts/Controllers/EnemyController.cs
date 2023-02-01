using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.States.Enemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
  private static int MAX_HEALTH = 100;

  public EnemyStartState StartState { get; } = new EnemyStartState();
  public EnemyFindTargetState FindTargetState { get; } = new EnemyFindTargetState();
  public EnemyEatTreeState EatTreeState { get; } = new EnemyEatTreeState();

  [SerializeField] private Transform destination;

  [FormerlySerializedAs("attackDelay")] [SerializeField]
  private float attackInterval = 5.0f;

  [SerializeField] private int attackStrength = 2;

  public IDamageable damageableTarget;
  public int Health { get; private set; }
  public Animator Animator { get; private set; }

  private NavMeshAgent _navMeshAgent;
  private BaseEnemyState _currentState;

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

  public void ResetEnemy()
  {
    Health = MAX_HEALTH;
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
}