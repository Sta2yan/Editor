using UnityEngine;

namespace Stanislav.Movement
{
    internal class Jump : MonoBehaviour, IJump
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _groundCheckPoint;
        [SerializeField, Min(0)] private float _gravityMultiplayer;
        [SerializeField] private float _groundCollisionDistance;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Vector3 _groundCollisionSize;
        [SerializeField] private LayerMask _groundCollisionMask;

        private Vector3 _velocity;
        private bool _ground;
        
        private void Update()
        {
            CheckGround();
            ControlGravity();
        }

        public bool TryJump()
        {
            if (_ground == false) return false;

            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            return true;
        }
        
        private void ControlGravity()
        {
            if (_ground && _velocity.y < 0f)
            {
                _velocity.y = 0f;
            }

            _velocity += Physics.gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
        
        private void CheckGround()
        {
            _ground = Physics.BoxCast(_groundCheckPoint.position, _groundCollisionSize / 2, Vector3.down, Quaternion.identity, _groundCollisionDistance, _groundCollisionMask);
        }
        
        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(_groundCheckPoint.position, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawRay(_groundCheckPoint.position + Vector3.left * _groundCollisionSize.x / 2f, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawRay(_groundCheckPoint.position + Vector3.right * _groundCollisionSize.x / 2f, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawRay(_groundCheckPoint.position + Vector3.forward * _groundCollisionSize.z / 2f, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawRay(_groundCheckPoint.position + Vector3.back * _groundCollisionSize.z / 2f, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawLine(_groundCheckPoint.position + new Vector3(-_groundCollisionSize.x / 2f, 0f, -_groundCollisionSize.z / 2f), _groundCheckPoint.position + new Vector3(_groundCollisionSize.x / 2f, 0f, -_groundCollisionSize.z / 2f), Color.yellow, 0f, false);
            Debug.DrawLine(_groundCheckPoint.position + new Vector3(-_groundCollisionSize.x / 2f, 0f, _groundCollisionSize.z / 2f), _groundCheckPoint.position + new Vector3(_groundCollisionSize.x / 2f, 0f, _groundCollisionSize.z / 2f), Color.yellow, 0f, false);
            Debug.DrawLine(_groundCheckPoint.position + new Vector3(-_groundCollisionSize.x / 2f, 0f, -_groundCollisionSize.z / 2f), _groundCheckPoint.position + new Vector3(-_groundCollisionSize.x / 2f, 0f, _groundCollisionSize.z / 2f), Color.yellow, 0f, false);
            Debug.DrawLine(_groundCheckPoint.position + new Vector3(_groundCollisionSize.x / 2f, 0f, -_groundCollisionSize.z / 2f), _groundCheckPoint.position + new Vector3(_groundCollisionSize.x / 2f, 0f, _groundCollisionSize.z / 2f), Color.yellow, 0f, false);
        }
    }
}
