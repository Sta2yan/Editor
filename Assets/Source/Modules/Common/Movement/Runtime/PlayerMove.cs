using UnityEngine;

namespace Stanislav.Movement
{
    internal class PlayerMove : MonoBehaviour, IMove
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _faceCamera;
        [SerializeField] private Transform _root;
        [SerializeField, Min(0)] private float _speed;
        
        public void Execute(float horizontal, float vertical)
        {
            Vector3 moveDirection = _root.right * horizontal + _root.forward * vertical;
            moveDirection = Quaternion.Euler(0f, _faceCamera.rotation.eulerAngles.y, 0f) * moveDirection;

            _characterController.Move(moveDirection.normalized * _speed * Time.deltaTime);
        }
    }
}
