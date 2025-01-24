using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "BubbleGame/InputReader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    public event UnityAction<Vector2> OnMoveEvent;
    
    private GameInput _gameInput;

    private void OnEnable()
    {
        // Create an instance of GameInput if it doesn't exist when this scriptable object gets enabled. Callbacks are
        // assigned to this object for the methods below. 
        if (_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
        }
        
        EnableAllInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void EnableAllInput() => _gameInput.Gameplay.Enable();
    public void DisableAllInput() => _gameInput.Gameplay.Disable();

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
