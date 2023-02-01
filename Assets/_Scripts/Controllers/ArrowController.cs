using UnityEngine;

public class ArrowController : MonoBehaviour
{
  public int ArrowStrength = 50;

  private void OnCollisionEnter(Collision collision)
  {
    // Debug.Log("Arrow Collided: " + collision.gameObject.name);
    EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
    if (enemy != null)
    {
      enemy.TakeDamage(ArrowStrength);
      // Destroy Arrow Right Away if hits target, else other scripts will destroy it
      Destroy(gameObject, 0.2f);
    }
  }
}