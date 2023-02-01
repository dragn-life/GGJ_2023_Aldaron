using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using Cinemachine;
using UnityEngine;

public class MouseController : MonoBehaviour
{
  public float HorizontalSpeed = 200;
  public float VerticalSpeed = 2;

  [SerializeField] private CinemachineFreeLook camera;

  [SerializeField] private GameManager gameManager;

  private bool _isToggleable;
  private bool _lockCams = false;

  private void OnEnable()
  {
    gameManager.StartGameEvent += GameStarted;
    gameManager.GameOverEvent += GameStopped;
    gameManager.VictoryEvent += GameStopped;
  }

  private void OnDisable()
  {
    gameManager.StartGameEvent -= GameStarted;
    gameManager.GameOverEvent -= GameStopped;
    gameManager.VictoryEvent -= GameStopped;
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape) && _isToggleable)
    {
      ToggleCameraLock(!_lockCams);
    }
  }

  private void GameStarted()
  {
    _isToggleable = true;
    ToggleCameraLock(false);
  }

  private void GameStopped()
  {
    _isToggleable = false;
    ToggleCameraLock(true);
  }

  private void ToggleCameraLock(bool isLocked)
  {
    _lockCams = isLocked;
    if (isLocked)
    {
      camera.m_XAxis.m_MaxSpeed = 0;
      camera.m_YAxis.m_MaxSpeed = 0;
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    }
    else
    {
      camera.m_XAxis.m_MaxSpeed = HorizontalSpeed;
      camera.m_YAxis.m_MaxSpeed = VerticalSpeed;
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
    }
  }
}