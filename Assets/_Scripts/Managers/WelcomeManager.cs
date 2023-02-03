using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeManager : MonoBehaviour
{
  [SerializeField] private string soloSceneName = "Single Player";
  [SerializeField] private string multiplayerSceneName = "Multi Player";

  [SerializeField] private GameObject settingsMenu;


  private async void Start()
  {
    try
    {
      await UnityServices.InitializeAsync();
      Analytics.TrackEvent("gameStarted");
    }
    catch (ConsentCheckException e)
    {
      Debug.LogError("Failed to Init Analytics" + e);
    }
  }

  public void LoadSolo()
  {
    Analytics.TrackEvent("loadSinglePlayerScene");
    SceneManager.LoadScene(soloSceneName);
  }

  public void LoadMultiplayer()
  {
    Analytics.TrackEvent("loadMultiplayerScene");
    SceneManager.LoadScene(multiplayerSceneName);
  }

  public void ShowSettings()
  {
    Analytics.TrackEvent("showSettings");
    settingsMenu.SetActive(true);
  }

  public void HideSettings()
  {
    Analytics.TrackEvent("hideSettings");
    settingsMenu.SetActive(false);
  }
}