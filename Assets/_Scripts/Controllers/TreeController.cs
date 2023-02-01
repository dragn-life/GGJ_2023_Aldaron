using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Controllers
{
  public class TreeController : MonoBehaviour, IDamageable
  {
    [SerializeField] private AncientTreeManager treeManager;

    public void TakeDamage(int amount)
    {
      treeManager.TakeDamage(amount);
    }
  }
}