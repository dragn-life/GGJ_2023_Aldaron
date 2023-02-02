using _Scripts.Managers;
using Cinemachine;
using UnityEngine;

public class MouseController : MonoBehaviour
{
  [SerializeField] private PlayerSettingsSO playerSettings;
  [SerializeField] private CinemachineFreeLook camera;

  [SerializeField] private GameManager gameManager;

  private bool _isToggleable;
  private bool _lockCams = false;

  private void OnEnable()
  {
    // Lock Camera Up by Default
    GameStopped();
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
      camera.m_XAxis.m_MaxSpeed = playerSettings.HorizontalSens;
      camera.m_YAxis.m_MaxSpeed = playerSettings.VerticalSens;
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
    }
  }
}