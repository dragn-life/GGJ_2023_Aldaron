using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
  [SerializeField] private float destroyDelay = 2.0f;

  // Start is called before the first frame update
  void Start()
  {
    Destroy(gameObject, destroyDelay);
  }
}