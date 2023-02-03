using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.Managers
{
  public class HUDManager : MonoBehaviour
  {
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DifficultyManagerSO difficultyManager;
    [SerializeField] private AncientTreeManager treeManager;
    [SerializeField] private CountDownManager countDownManager;

    [SerializeField] private GameObject startScreen;

    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject victoryNextLevelButton;
    [SerializeField] private GameObject victoryRestartButton;
    [SerializeField] private TextMeshProUGUI victoryLevelText;


    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject hudScreen;
    [SerializeField] private Slider treeHealth;
    [SerializeField] private TextMeshProUGUI countdown;

    private void OnEnable()
    {
      treeManager.HealthChangeEvent += UpdateHealthSlider;

      gameManager.StartGameEvent += SetupPlayHud;
      gameManager.VictoryEvent += SetupVictoryHud;
      gameManager.GameOverEvent += SetupGameOverHud;

      countDownManager.TimeChangeEvent += UpdateCountdown;
    }

    private void UpdateCountdown(float time)
    {
      countdown.text = "Time Until Dawn: " + time;
    }

    private void OnDisable()
    {
      treeManager.HealthChangeEvent -= UpdateHealthSlider;

      gameManager.StartGameEvent -= SetupPlayHud;
      gameManager.VictoryEvent -= SetupVictoryHud;
      gameManager.GameOverEvent -= SetupGameOverHud;
    }

    private void UpdateHealthSlider(int health)
    {
      treeHealth.value = health;
    }

    private void SetupGameOverHud()
    {
      ResetScreen();
      gameOverScreen.SetActive(true);
    }

    private void SetupVictoryHud()
    {
      ResetScreen();
      victoryScreen.SetActive(true);
      if (victoryLevelText)
      {
        victoryLevelText.text = difficultyManager.CurrentDifficultyLevel().ToString();
      }

      if (difficultyManager.IsOnLastDifficulty())
      {
        victoryRestartButton?.SetActive(true);
      }
      else
      {
        victoryNextLevelButton?.SetActive(true);
      }
    }

    private void SetupPlayHud()
    {
      ResetScreen();
      hudScreen.SetActive(true);
    }

    private void ResetScreen()
    {
      startScreen.SetActive(false);
      victoryScreen.SetActive(false);
      victoryNextLevelButton?.SetActive(false);
      victoryRestartButton?.SetActive(false);
      gameOverScreen.SetActive(false);
      hudScreen.SetActive(false);
    }

    public void GotoNextLevel()
    {
      difficultyManager.GotoNextDifficulty();
      Scene scene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(scene.name);
    }

    public void RestartGame()
    {
      difficultyManager.ResetDifficulty();
      Scene scene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(scene.name);
    }
  }
}