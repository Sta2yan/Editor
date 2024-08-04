using UnityEngine;

namespace Stanislav.FPSCamera
{
    internal class FirstPersonCamera : MonoBehaviour, IFirstPersonCamera
    {
        private const float RotateAcceleration = 100f;

        [SerializeField] private Transform _root;
        [SerializeField, Range(0, 100)] private float _sensitivity = 3f;
        [SerializeField, Min(0)] private float _lerpSmooth = 100f;
        [SerializeField] private Vector2 _boundRotationY = new(-80f, 90f);

        private float _rotationY;
        private float _rotationX;

        public void Turn(float horizontal, float vertical)
        {
            _rotationY += horizontal * _sensitivity * RotateAcceleration * Time.deltaTime;
            _rotationX += vertical * _sensitivity * RotateAcceleration * Time.deltaTime;

            _rotationX = Mathf.Clamp(_rotationX, _boundRotationY.x, _boundRotationY.y);

            _root.localRotation = Quaternion.Lerp(_root.localRotation, Quaternion.Euler(-_rotationX, _rotationY, 0),
                _lerpSmooth);
        }

        #region Чисто для дз (такого метода здесь быть не должно :) )

        private string _text;
        
        private void ChangeObjectName()
        {
            gameObject.name = _text;
        }

        #endregion
    }
}
