using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayscaleDisabler : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().material.SetInt("_IsGrayscale", 0);
    }
}
