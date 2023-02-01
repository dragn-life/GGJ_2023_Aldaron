using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  [SerializeField] private CameraReferences cameraReferences;
  [SerializeField] private CinemachineFreeLook mainCamera;
  [SerializeField] private CinemachineFreeLook aimCamera;

  // Start is called before the first frame update
  void Start()
  {
    cameraReferences.mainCamera = mainCamera;
    cameraReferences.aimCamera = aimCamera;
  }
}