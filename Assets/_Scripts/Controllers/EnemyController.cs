using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.States.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
  private static int MAX_HEALTH = 100;

  public EnemyStartState StartState { get; } = new EnemyStartState();
  public EnemyFindTargetState FindTargetState { get; } = new EnemyFindTargetState();

  [SerializeField] private Transform destination;

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

  public void SwitchState(BaseEnemyState newState)
  {
    _currentState = newState;
    _currentState.EnterState(this);
  }

  public void ResetEnemy()
  {
    Health = MAX_HEALTH;
  }

  public void GotoDestination()
  {
    _navMeshAgent.destination = destination.position;
  }
}