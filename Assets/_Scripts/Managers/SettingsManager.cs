using System;
using Coherence.Log;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
  [SerializeField] private PlayerSettingsSO playerSettings;

  [SerializeField] private Slider HSenseSlider;
  [SerializeField] private Slider VSenseSlider;

  private void Start()
  {
    HSenseSlider.value = playerSettings.HorizontalSens;
    VSenseSlider.value = playerSettings.VerticalSens;
  }

  private void OnEnable()
  {
    HSenseSlider.onValueChanged.AddListener(UpdateHorizontalSens);
    VSenseSlider.onValueChanged.AddListener(UpdateVerticalSens);
  }

  private void OnDisable()
  {
    HSenseSlider.onValueChanged.RemoveListener(UpdateHorizontalSens);
    VSenseSlider.onValueChanged.RemoveListener(UpdateVerticalSens);
  }

  public void UpdateHorizontalSens(float newSens)
  {
    Debug.Log("New Sens: " + newSens);
    playerSettings.HorizontalSens = newSens;
  }

  public void UpdateVerticalSens(float newSens)
  {
    playerSettings.VerticalSens = newSens;
  }
}