using System;
using Stanislav.Movement;
using UnityEngine;

public class MovementTestInput : MonoBehaviour
{
    [Tooltip("Connect IMove")]
    [SerializeField] private MonoBehaviour _playerMove;
    [Tooltip("Connect IJump")]
    [SerializeField] private MonoBehaviour _jump;
    
    private IMove PlayerMove => _playerMove as IMove;
    private IJump Jump => _jump as IJump;
    
    private void OnValidate()
    {
        if (_playerMove != null && _playerMove is not IMove)
            throw new InvalidOperationException(nameof(_playerMove) + $" is not {nameof(IMove)}!");
        
        if (_jump != null && _jump is not IJump)
            throw new InvalidOperationException(nameof(_jump) + $" is not {nameof(IJump)}!");
    }

    private void Update()
    {
        PlayerMove.Execute(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if (Input.GetButtonDown("Jump"))
            Jump.TryJump();
    }
}
