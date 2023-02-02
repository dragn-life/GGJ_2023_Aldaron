using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeManager : MonoBehaviour
{
  [SerializeField] private string soloSceneName = "Single Player";
  [SerializeField] private string multiplayerSceneName = "Multi Player";

  [SerializeField] private GameObject settingsMenu;


  public void LoadSolo()
  {
    SceneManager.LoadScene(soloSceneName);
  }

  public void LoadMultiplayer()
  {
    SceneManager.LoadScene(multiplayerSceneName);
  }

  public void ShowSettings()
  {
    settingsMenu.SetActive(true);
  }

  public void HideSettings()
  {
    settingsMenu.SetActive(false);
  }
}