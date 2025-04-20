using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class InputEventExample : MonoBehaviour
{
    [SerializeField]private PlayerAgentMove _playerAgentMove;
    [SerializeField] private float _doubleClickTime = 0.3f;
    [SerializeField] private Camera _cam;
    private float _lastClickTime;
    private int _clickCount;

    public void OnInteractPressed(InputAction.CallbackContext context)    
    {
        if (context.performed)
        {
            float timeSinceLastClick = Time.time - _lastClickTime;
            
            if (timeSinceLastClick <= _doubleClickTime)
            {
                _clickCount++;
                if (_clickCount == 2)
                {
                    _playerAgentMove.SetMovementState(PlayerAgentMove.MovementState.Run);
                    _clickCount = 0;
                }
            }
            else
            {
                _playerAgentMove.SetMovementState(PlayerAgentMove.MovementState.Walk);
                _clickCount = 1;
            }

            _lastClickTime = Time.time;
        }
        ChangePoint();
    }
    
   
    private void ChangePoint()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
            
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ground"))
        {
            _playerAgentMove.SetMovementInput(PlayerAgentMove.InputMetod.Mouse);
            _playerAgentMove.PlayerPointToMove = hit.point;
        }
    }
}