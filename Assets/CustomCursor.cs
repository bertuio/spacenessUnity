using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(AudioSource))]
public class CustomCursor : DontDestroy
{
    private AudioSource _source;
    [SerializeField] private AudioClip[] _mouseClickSound;
    [SerializeField] private Texture2D _cursorIdle;
    [SerializeField] private Texture2D _cursorClicked;
    private InputAction leftButtonPress, leftButtonRelease;
    private static CustomCursor _cursor;

    public static void HideCursor() 
    {
        _cursor.leftButtonPress.Disable();
        _cursor.leftButtonRelease.Disable();
        Cursor.visible = false;
        _cursor._source.mute = true;
    }
    public static void ShowCursor()
    {
        _cursor.leftButtonPress.Enable();
        _cursor.leftButtonRelease.Enable();
        Cursor.visible = true;
        _cursor._source.mute = false;
    }

    public static void Mute() 
    {
        _cursor._source.mute = true;
    }

    private void Awake()
    {
        if (!_cursor)
        {
            _cursor = this;
        }
        else {
            Destroy(gameObject);
        }

        _source = GetComponent<AudioSource>();
        InitializeCursor();
    }

    private void InitializeCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(_cursorIdle, Vector2.zero, CursorMode.Auto);

        leftButtonPress = new InputAction();
        leftButtonPress.AddBinding("<Mouse>/leftButton", $"Press(behavior={(int)PressBehavior.PressOnly})");
        Debug.Log(leftButtonPress.bindings[0].interactions);
        leftButtonPress.performed += ClickCallback;

        leftButtonRelease = new InputAction();
        leftButtonRelease.AddBinding("<Mouse>/leftButton", $"Press(behavior={(int)PressBehavior.ReleaseOnly})");
        leftButtonRelease.performed += CursorReleaseCallback;
        HideCursor();
    }

    private void ClickCallback(InputAction.CallbackContext context)
    {
        if (_source && _mouseClickSound.Length > 0)
            _source.clip = _mouseClickSound[Random.Range(0, _mouseClickSound.Length)];
            _source.Play();

        Cursor.SetCursor(_cursorClicked, Vector2.zero, CursorMode.Auto);
        Debug.Log("Pressed");
    }

    private void CursorReleaseCallback(InputAction.CallbackContext context)
    {
        Debug.Log("Released");
        Cursor.SetCursor(_cursorIdle, Vector2.zero, CursorMode.Auto);
    }
}
