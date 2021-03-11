using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MineralResearchBoulderGrabber : MonoBehaviour
{
    [SerializeField] private InputAction _shot;
    public Action OnBoulderGrabbed;

    private void Awake()
    {
        _shot.performed += TraceBoulder;
    }

    private void OnDisable()
    {
        _shot.Disable();
    }
    private void OnEnable()
    {
        _shot.Enable();
    }

    private void TraceBoulder(InputAction.CallbackContext context) 
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out MineralResearchBoulder boulder)) 
            {
                boulder.Grab();
                OnBoulderGrabbed?.Invoke();
            }
        }
    }
}
