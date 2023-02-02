using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerSettingsSO : ScriptableObject
{
  public float HorizontalSens;
  public float VerticalSens;
}