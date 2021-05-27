using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MineralResearchBoulderGrabber : MonoBehaviour
{
    [SerializeField] private InputAction _shot;
    [SerializeField] private InputAction _mouseMove;
    [SerializeField] private WidgetDrawer _drawer;

    private float _mouseMoveDelay = 0.1f;
    private float _lastTime = 0;
    public event Action OnRareBoulderGot;
    private const string LAYER_MASK_NAME = "Minigame Tracables";
    private MineralResearchBoulder _currentBoulder;

    public void Activate()
    {
        _shot.Enable();
        _mouseMove.Enable();
    }
    public void Deactivate()
    {
        _shot.Disable();
        _mouseMove.Disable();
    }

    private void Awake()
    {
        _shot.performed += ShotHandler;
        _mouseMove.performed += MouseMoveHandler;
    }

    private void OnDisable()
    {
        Deactivate();
    }

    private void MouseMoveHandler(InputAction.CallbackContext context)
    {
        if (Time.time - _lastTime < _mouseMoveDelay) 
        {
            return;
        }
        _lastTime = Time.time;
        _currentBoulder = TraceBoulder();
        if (_currentBoulder)
        {
            _drawer.Show();
            _drawer.UpdateBoulder(_currentBoulder);
        }
        else 
        {
            _drawer.Hide();
        }
    }

    private void ShotHandler(InputAction.CallbackContext context)
    {
        if (_currentBoulder)
        {
            if (_currentBoulder.Rare)
            {
                OnRareBoulderGot?.Invoke();
            }
            _currentBoulder.Grab();
        }
        _drawer.Hide();
    }

    private MineralResearchBoulder TraceBoulder() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, ~LayerMask.NameToLayer(LAYER_MASK_NAME)))
        {
            Debug.Log(hit.transform.gameObject);
            if (hit.transform.TryGetComponent(out MineralResearchBoulder boulder)) 
            {
                return boulder;
            }
        }

        return null;
    }
}
