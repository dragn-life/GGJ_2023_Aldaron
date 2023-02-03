using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using _Scripts.States.Enemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour, IDamageable
{
  private static int MAX_HEALTH = 100;

  public EnemyStartState StartState { get; } = new EnemyStartState();
  public EnemySpawnState SpawnState { get; } = new EnemySpawnState();
  public EnemyFindTargetState FindTargetState { get; } = new EnemyFindTargetState();
  public EnemyEatTreeState EatTreeState { get; } = new EnemyEatTreeState();
  public EnemyDamageState DamageState { get; } = new EnemyDamageState();
  public EnemyDeathState DeathState { get; } = new EnemyDeathState();

  [SerializeField] private GameObject spawnParticle;
  [SerializeField] private GameObject deathParticle;

  [SerializeField] private Transform destination;


  [SerializeField] private int attackStrength = 2;

  public bool ShouldBeDead;
  public GameManager GameManager;
  public IDamageable DamageableTarget;
  public float attackInterval = 5.0f;

  // public int Health { get; private set; }
  public int Health;
  public Animator Animator { get; private set; }

  public NavMeshAgent navMeshAgent { get; private set; }

  private BaseEnemyState _currentState;
  private BaseEnemyState _lastState;

  private bool _canTakeDamage = true;

  private void OnEnable()
  {
    Animator = GetComponent<Animator>();
    navMeshAgent = GetComponent<NavMeshAgent>();

    SubscribeToGameEvents();
  }

  public void SubscribeToGameEvents()
  {
    if (GameManager)
    {
      GameManager.StartGameEvent += StartGame;
      GameManager.VictoryEvent += GameOver;
      GameManager.GameOverEvent += GameOver;
    }
  }

  private void StartGame()
  {
    SwitchState(StartState);
  }

  private void GameOver()
  {
    ShouldBeDead = true;
    SwitchState(DeathState);
  }

  private void Update()
  {
    _currentState?.OnUpdate(this);
  }

  private void LateUpdate()
  {
    _currentState?.OnLateUpdate(this);
  }

  private void OnCollisionEnter(Collision collision)
  {
    _currentState?.OnCollisionEnter(collision, this);
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
    navMeshAgent.destination = destination.position;
  }

  public void SetNextDestination(Transform nextDestination)
  {
    destination = nextDestination;
  }

  public bool DetectTree(Collision collision)
  {
    IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
    if (damageable != null)
    {
      DamageableTarget = damageable;
      return true;
    }

    return false;
  }

  private bool DetectTreeExit(Collision collision)
  {
    IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
    if (damageable != null)
    {
      DamageableTarget = null;
      return true;
    }

    return false;
  }

  public void EatTree()
  {
    DamageableTarget?.TakeDamage(attackStrength);
  }

  public void PlayDeathEffects()
  {
    // Instantiate(deathParticle, transform);
  }

  public void PlaySpawnEffects(float playTime)
  {
    // GameObject particle = Instantiate(spawnParticle, transform);
    // Destroy(particle, playTime);
  }

  public void FreezeAnimation()
  {
    Animator.speed = 0.0f;
  }

  public void FreezeAnimation(float delay)
  {
    Invoke(nameof(FreezeAnimation), delay);
  }

  public void TakeDamage(int amount)
  {
    const float debounceTime = 1.0f;

    if (_canTakeDamage)
    {
      _canTakeDamage = false;
      Health -= amount;
      // Debug.Log("Enemy Health" + Health);
      if (Health > 0)
      {
        SwitchState(DamageState);
        Invoke(nameof(EnableCanTakeDamage), debounceTime);
      }
      else
      {
        SwitchState(DeathState);
      }
    }
  }

  private void EnableCanTakeDamage()
  {
    _canTakeDamage = true;
  }
}