using System;
using _Scripts.States.Player;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public PlayerStartState StartState { get; } = new PlayerStartState();
  public PlayerIdleState IdleState { get; } = new PlayerIdleState();
  public PlayerMoveState MoveState { get; } = new PlayerMoveState();
  public PlayerShootState ShootState { get; } = new PlayerShootState();

  public Animator Animator { get; private set; }

  [SerializeField] private GameObject arrowPrefab;
  [SerializeField] private Transform arrowSpawnPosition;
  [SerializeField] private float arrowForce = 200f;
  public float arrowShootDelay = 1.5f;

  [SerializeField] private float moveSpeed = 10f;

  [SerializeField] private CameraReferences cameraReferences;

  private BasePlayerState _lastState;
  private BasePlayerState _currentState;
  private Camera _currentCamera;

  // Start is called before the first frame update
  void Start()
  {
    Animator = GetComponent<Animator>();
    _currentCamera = Camera.main;
    SwitchState(StartState);
  }

  // Update is called once per frame
  void Update()
  {
    _currentState.OnUpdate(this);
  }

  private void LateUpdate()
  {
    _currentState.OnLateUpdate(this);
  }

  public void AimWithCamera()
  {
    Vector3 lookDirection = _currentCamera.transform.forward;
    lookDirection.y = 0;

    Quaternion rotation = Quaternion.LookRotation(lookDirection);
    transform.rotation = rotation;
  }

  public void SwitchState(BasePlayerState state)
  {
    _lastState = _currentState;
    _currentState = state;
    _currentState.EnterState(this);
  }

  public void SwitchToLastState()
  {
    _currentState = _lastState;
    _currentState.EnterState(this);
  }

  public bool CheckShooting()
  {
    return Input.GetButtonDown("Fire1");
  }

  public void HandleAim()
  {
    if (Input.GetButtonDown("Fire2"))
    {
      cameraReferences.aimCamera.Priority = 3;
    }

    if (Input.GetButtonUp("Fire2"))
    {
      cameraReferences.aimCamera.Priority = 1;
    }
  }

  public void ShootArrow()
  {
    // Ray Cast Direction to Shoot
    Transform lookAt = null;
    
    // Experimental , doesn't work
    // RaycastHit hit;
    // Ray ray = _currentCamera.ScreenPointToRay(Input.mousePosition);
    // if (Physics.Raycast(ray, out hit))
    // {
    //   lookAt = hit.transform;
    // }

    GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPosition.position, Quaternion.identity);
    Rigidbody arrowRigidbody = arrow.GetComponent<Rigidbody>();

    if (lookAt != null)
    {
      arrow.transform.rotation = Quaternion.LookRotation(lookAt.position);
    }
    else
    {
      arrow.transform.rotation = arrowSpawnPosition.rotation;
    }

    arrowRigidbody.AddForce(arrow.transform.forward * arrowForce, ForceMode.Impulse);

    SwitchState(IdleState);
  }

  public bool MovePlayer()
  {
    bool isPlayerMoving = false;
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    // Camera Direction
    Vector3 camRight = new Vector3(_currentCamera.transform.right.x, 0f, _currentCamera.transform.right.z);
    Vector3 camForward = new Vector3(_currentCamera.transform.forward.x, 0f, _currentCamera.transform.forward.z);

    Vector3 movement = camRight * horizontalInput + camForward * verticalInput;

    // IsPlayerMoving = horizontalInput != 0 || verticalInput != 0;
    isPlayerMoving = movement.magnitude > 0;

    // Moving
    if (isPlayerMoving)
    {
      movement.Normalize();
      movement *= moveSpeed * Time.deltaTime;
      transform.Translate(movement, Space.World);

      transform.forward = movement;
    }

    return isPlayerMoving;
  }

  public void ResetAnimations()
  {
    Animator.SetBool("idle2", false);
    Animator.SetBool("run", false);
  }
}