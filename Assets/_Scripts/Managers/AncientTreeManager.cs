using System;
using UnityEngine;

namespace _Scripts.Managers
{
  public class AncientTreeManager : MonoBehaviour, IDamageable
  {
    private const int MAX_HEALTH = 100;

    [SerializeField] private GameManager gameManager;

    public event Action<int> HealthChangeEvent;
    
    public int Health { get; private set; }

    private void OnEnable()
    {
      StartGame();
    }

    private void StartGame()
    {
      Health = 100;
    }

    public void TakeDamage(int amount)
    {
      Health -= amount;
      HealthChangeEvent?.Invoke(Health);
      if (Health <= 0)
      {
        gameManager.LoseGame();
      }
    }
  }
}