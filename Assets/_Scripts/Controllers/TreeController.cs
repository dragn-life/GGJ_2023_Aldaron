using UnityEngine;

namespace _Scripts.Controllers
{
  public class TreeController : MonoBehaviour, IDamageable
  {
    public int health = 100;

    public void TakeDamage(int amount)
    {
      health -= amount;
    }
  }
}