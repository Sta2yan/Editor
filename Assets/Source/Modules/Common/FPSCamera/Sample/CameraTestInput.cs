using System;
using UnityEngine;

namespace Stanislav.FPSCamera.Sample
{
    public class CameraTestInput : MonoBehaviour
    {
        [Tooltip("Connect IFirstPersonCamera")]
        [SerializeField] private MonoBehaviour _firstPersonCameraMovement;

        private IFirstPersonCamera FirstPersonCamera => _firstPersonCameraMovement as IFirstPersonCamera;
        
        private void OnValidate()
        {
            if (_firstPersonCameraMovement != null && _firstPersonCameraMovement is not IFirstPersonCamera)
                throw new InvalidOperationException(nameof(_firstPersonCameraMovement) + $" is not {nameof(IFirstPersonCamera)}!");
        }

        private void Update()
        {
            FirstPersonCamera.Turn(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}
