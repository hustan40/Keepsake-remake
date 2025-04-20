using UnityEngine;
using UnityEngine.InputSystem;

public class MovementKeyBoard : MonoBehaviour
{
    [SerializeField] private PlayerAgentMove _playerAgentMove;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _moveSpeed = 5f;
    
    private Vector2 _currentInput;
    private Vector3 _cameraForward;
    private Vector3 _cameraRight;
    private Coroutine _movementCoroutine;

    private void Awake()
    {
        if (_mainCamera == null) _mainCamera = Camera.main;
        UpdateCameraVectors();
    }

    public void MovementRead(InputAction.CallbackContext context)
    {
        if (context.started || _currentInput !=context.ReadValue<Vector2>())
        {
            _currentInput = context.ReadValue<Vector2>();
            if (_movementCoroutine != null) StopCoroutine(_movementCoroutine);
            _movementCoroutine = StartCoroutine(MoveContinuous());
        }
        else if (context.canceled)
        {
            _currentInput = Vector2.zero;
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
                _movementCoroutine = null;
            }
        }
    }
    public void AddSpeed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _playerAgentMove.SetMovementState(PlayerAgentMove.MovementState.Run);
            _moveSpeed=5f;
        }
        if(context.canceled)
        {
            _playerAgentMove.SetMovementState(PlayerAgentMove.MovementState.Walk);
            _moveSpeed=2;
        }
    }

    private System.Collections.IEnumerator MoveContinuous()
    {
        while (_currentInput != Vector2.zero)
        {
            UpdateCameraVectors();
            Vector3 movement = (_cameraForward * _currentInput.y + _cameraRight * _currentInput.x).normalized;
            _playerAgentMove.SetMovementInput(PlayerAgentMove.InputMetod.Keyboard);
            _playerAgentMove.PlayerPointToMove += movement * _moveSpeed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private void UpdateCameraVectors()
    {
        _cameraForward = _mainCamera.transform.forward;
        _cameraRight = _mainCamera.transform.right;
        _cameraForward.y = 0;
        _cameraRight.y = 0;
        _cameraForward.Normalize();
        _cameraRight.Normalize();
    }
}