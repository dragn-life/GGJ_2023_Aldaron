using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerState
{
  public abstract void EnterState(PlayerController player);
  public abstract void OnUpdate(PlayerController player);
  public abstract void OnLateUpdate(PlayerController player);
}