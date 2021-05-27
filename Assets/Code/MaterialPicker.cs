using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaterialPicker : MonoBehaviour
{
    [SerializeField] private InputAction _input;
    [SerializeField] private Transform _rayPivot;

    public Action<AnalizeMaterial> OnMaterialPicked;

    private void Start()
    {
        _input.performed += Shoot;
    }

    private void OnDisable()
    {
        Deactivate();
    }

    public void Activate()
    {
        _input.Enable();
    }

    public void Deactivate()
    {
        _input.Disable();
    }

    private void Shoot(InputAction.CallbackContext context) 
    {
        Debug.Log("shot");
        RaycastHit hit; 
        bool hitted = Physics.Raycast(_rayPivot.position, _rayPivot.right, out hit, 4);
        if (hitted && hit.transform.TryGetComponent(out AnalizeMaterial material))
        {
            OnMaterialPicked.Invoke(material);
            Debug.Log("picked");
        }
    }
}
