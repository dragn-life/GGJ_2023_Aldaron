using System.Collections.Generic;
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
    [SerializeField] private TextMeshProUGUI startLevelText;

    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject victoryNextLevelButton;
    [SerializeField] private GameObject victoryRestartButton;
    [SerializeField] private Slider victoryLevelSlider;
    [SerializeField] private List<GameObject> victoryLevelNotches;
    [SerializeField] private Sprite activeVictoryNotchSprite;
    [SerializeField] private Sprite inactiveVictoryNotchSprite;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverLevelText;

    [SerializeField] private GameObject hudScreen;
    [SerializeField] private Slider treeHealth;
    [SerializeField] private TextMeshProUGUI countdown;

    private void Start()
    {
      if (startLevelText)
      {
        startLevelText.text = difficultyManager.CurrentDifficultyLevel().ToString();
      }
    }

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
      if (victoryLevelSlider)
      {
        victoryLevelSlider.value = difficultyManager.CurrentDifficultyLevel() - 1;

        for (int i = 0; i < victoryLevelNotches.Count; i++)
        {
          Image image = victoryLevelNotches[i].GetComponent<Image>();
          // TODO: Fix why text is always null?
          TextMeshProUGUI text = victoryLevelNotches[i].GetComponentInChildren<TextMeshProUGUI>();
          if (i < difficultyManager.CurrentDifficultyLevel())
          {
            if (text)
            {
              text.color = new Color(214, 11, 11);
            }

            image.sprite = activeVictoryNotchSprite;
          }
          else
          {
            if (text)
            {
              text.color = new Color(255, 255, 255);
            }

            image.sprite = inactiveVictoryNotchSprite;
          }
        }
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