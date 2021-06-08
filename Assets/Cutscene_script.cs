using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Cutscene_script : MonoBehaviour
{
    float _additiveFov = -55;
    private Camera cam;
    [SerializeField] private Material _skybox;
    private void Start()
    {
        RenderSettings.skybox.color = Color.white * 2/3;
          cam = GetComponent<Camera>();
        Character.GetCharacter().LockMovementAndRotation();
        Character.GetCharacter().AttachedCameraController.SimulateCamera(GetComponent<MinigameCamera>());
    }

    private void Update()
    {
        cam.transform.position -= Vector3.right * Time.deltaTime * 1.4f;
        Character.GetCharacter().AttachedCameraController.SetAdditiveFov(_additiveFov);
        _additiveFov -= 0.03f * Time.deltaTime;
        RenderSettings.skybox.color = Color.Lerp(RenderSettings.skybox.color, Color.black, 0.17f * Time.deltaTime);
    }
}
