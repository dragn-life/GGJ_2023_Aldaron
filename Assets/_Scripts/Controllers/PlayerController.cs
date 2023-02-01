using System.Collections;
using System.Collections.Generic;
using _Scripts.States.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public PlayerStartState StartState { get; } = new PlayerStartState();
  public PlayerIdleState IdleState { get; } = new PlayerIdleState();
  public PlayerMoveState MoveState { get; } = new PlayerMoveState();

  public Animator Animator { get; private set; }
  public bool IsPlayerMoving { get; private set; }
  
  [SerializeField] private float moveSpeed = 10f;

  private BasePlayerState _currentState;

  // Start is called before the first frame update
  void Start()
  {
    Animator = GetComponent<Animator>();
    SwitchState(StartState);
  }

  // Update is called once per frame
  void Update()
  {
    _currentState.OnUpdate(this);
  }

  public void SwitchState(BasePlayerState state)
  {
    _currentState = state;
    _currentState.EnterState(this);
  }

  public void MovePlayer()
  {
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    IsPlayerMoving = horizontalInput != 0 || verticalInput != 0;
  }
}