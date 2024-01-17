using UnityEngine;

public class DissolveManager : MonoBehaviour
{
    [SerializeField] private CameraRig cameraRig;

    private void Start()
    {
        cameraRig.OnManualRotate += CameraRotate;
    }

    private void CameraRotate(float angle)
    {
        
    }
}