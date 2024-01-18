using System.Collections.Generic;
using UnityEngine;

public class DissolveManager : MonoBehaviour
{
    [SerializeField] private CameraRig cameraRig;
    [SerializeField] private List<Sector> sectors;

    private float _angleStep;

    private void Awake()
    {
        _angleStep = cameraRig.angle;
        cameraRig.OnManualRotate += CameraRotate;
    }

    private void CameraRotate(float angle)
    {
        foreach (var sector in sectors)
        {
            var difference = Mathf.Abs(Mathf.DeltaAngle(sector.angle, angle));
            sector.SwitchObjects(difference <= _angleStep / 2);
        }
    }
}