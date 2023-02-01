using UnityEngine;

public class ArrowController : MonoBehaviour
{
  public int ArrowStrength = 50;

  private void OnCollisionEnter(Collision collision)
  {
    Debug.Log("Arrow Collided: " + collision.gameObject.name);
    IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
    if (damageable != null)
    {
      damageable.TakeDamage(ArrowStrength);
    }
  }
}