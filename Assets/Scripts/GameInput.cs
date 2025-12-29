using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput instance;
    public event EventHandler onMenuButtonPressed;

    private InputActions inputAction;
    private void Awake()
    {
        instance = this;
        inputAction = new InputActions();
        inputAction.Enable();

        inputAction.Lander.Menu.performed += Menu_performed;
    }

    private void Menu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onMenuButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        inputAction.Disable();
    }

    public bool isUpActionPressed()
    {
        return inputAction.Lander.LanderUp.IsPressed();
    }
    public bool isRightActionPressed()
    {
        return inputAction.Lander.LanderRight.IsPressed();
    }
    public bool isLeftActionPressed()
    {
        return inputAction.Lander.LanderLeft.IsPressed();
    }
}
