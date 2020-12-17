using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutineShaderModifier : MonoBehaviour
{
    private Material tempMaterial;
    private MeshRenderer meshRenderer;
    bool initialized;
    [SerializeField]
    private bool _IsGrayscale;
    [SerializeField]
    private bool _IsGrayscaleExcludeLight;
    void OnValidate()
    {
        if (Application.isPlaying & initialized) UpdateGrayscaleProperties();
    }
    void OnEnable()
    {
        initialized = true;
        meshRenderer = GetComponent<MeshRenderer>();
        tempMaterial = new Material(meshRenderer.material);
        meshRenderer.material = tempMaterial;
        UpdateGrayscaleProperties();
    }
    private void UpdateGrayscaleProperties()
    {
        setPropertyInt("_IsGrayscale", _IsGrayscale);
        setPropertyInt("_IsGrayscaleExcludeLight", _IsGrayscaleExcludeLight);
    }

    private void setPropertyInt(string name, bool value)
    {
        meshRenderer.material.SetInt(name, value?1:0);
    }
}