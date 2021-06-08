using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IngameMenu : MonoBehaviour
{
    private InputAction _escapeAction;
    private static IngameMenu _menu;
    private float _timescale;
    [SerializeField] private Canvas _menuCanvas;
    private void Awake()
    {
        //DontDestroyOnLoad(this);
        if (!_menu)
        {
            _menu = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _escapeAction = new InputAction();
        _escapeAction.AddBinding("<Keyboard>/escape");
        _escapeAction.performed += Activate;
        _escapeAction.Enable();
    }

    private void Activate(InputAction.CallbackContext context) 
    {
        if (_menuCanvas)
        {
            if (_menuCanvas.enabled)
            {
                Continue();
                return;
            }

            CustomCursor.ShowCursor();
            Character.GetCharacter().LockMovementAndRotation();
            _timescale = Time.timeScale;
            Time.timeScale = 0;

            _menuCanvas.enabled = true;
        }
    }

    public void Exit()
    {
        Application.Quit(0);
    }

    public void Continue()
    {
        Character.GetCharacter().UnlockMovement();
        CustomCursor.HideCursor();
        _menuCanvas.enabled = false;
        Time.timeScale = _timescale;
    }
}
