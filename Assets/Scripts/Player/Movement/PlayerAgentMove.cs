using System.CodeDom.Compiler;
using UnityEngine;
using UnityEngine.AI;


public class PlayerAgentMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _player;
    [SerializeField] private Transform _playerPointToMove;
    [SerializeField] private float _walkSpeed, _runSpeed,_checkDistance;
    [SerializeField]private LayerMask _layerGround;
    public enum MovementState {Walk, Run}
    public enum InputMetod {Mouse, Keyboard}
    private bool _lastInputIsMouse;

    public void SetMovementState(MovementState state)
    {
        switch (state)
        {
            case MovementState.Walk: _player.speed = _walkSpeed; break;
            case MovementState.Run: _player.speed = _runSpeed; break;
        }
    }

    public void SetMovementInput(InputMetod state)
    {
        switch (state)
        {
            case InputMetod.Mouse: _lastInputIsMouse = true; break;
            case InputMetod.Keyboard: CheckInput(); break;
        }
    }

    public Vector3 PlayerPointToMove
    {
        get => _playerPointToMove.position;
        set
        {
            if(_playerPointToMove.position != value)
            {
                _playerPointToMove.position = value;
                OnPontChange(_playerPointToMove.position);
            }
        }
    }
    private void OnPontChange(Vector3 _point)
    {
        CheckAlt();
        _player.SetDestination(_point);
    }

    private void CheckAlt()
    {
        if (!UpGrounded() & UnderGrounded())
        {
            _playerPointToMove.position+=Vector3.up * _checkDistance;
        }
        else if (UpGrounded() & !UnderGrounded())
        {
            _playerPointToMove.position-=Vector3.up * _checkDistance;
        }        
    }
    private bool UpGrounded()
    {
        return Physics.Raycast(_playerPointToMove.position,Vector3.down,_checkDistance,_layerGround);
    }
    private bool UnderGrounded()
    {
        return Physics.Raycast(_playerPointToMove.position,Vector3.up,_checkDistance,_layerGround);
    }

    private void CheckInput()
    {
        if (_lastInputIsMouse == true)
        {
            _lastInputIsMouse = false;
            _playerPointToMove.position = _player.transform.position;
        }
    }
}
