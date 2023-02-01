using Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraReferences", menuName = "ScriptableObjects/CameraReferences")]
public class CameraReferences : ScriptableObject
{
  public CinemachineFreeLook mainCamera;
  public CinemachineFreeLook aimCamera;
  
}
