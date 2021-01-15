using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveCamera : MonoBehaviour
{
    [SerializeField]
    private Camera parentalCamera;
    [SerializeField]
    private Camera additiveCamera;
    void Update()
    {
        additiveCamera.fieldOfView = parentalCamera.fieldOfView;
    }
}
