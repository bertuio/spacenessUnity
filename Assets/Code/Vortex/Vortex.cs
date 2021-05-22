using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(SphereCollider))]
public class Vortex : MonoBehaviour
{
    [SerializeField] private bool _isEnter = true;
    [SerializeField] private string _sceneName = "Vortex1";
    [SerializeField] private float _entranceRadius;
    private SphereCollider _collider;
    private const int CAMERA_FOV_ADDITION = 30;
    private const float MIN_TIMESCALE = .4f;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _entranceRadius);
    }

    private void OnTriggerStay(Collider other)
    {
        float distance = Vector3.Distance(Vector3.ProjectOnPlane(other.transform.position, Vector3.up), Vector3.ProjectOnPlane(transform.position, Vector3.up));

        int fov = (int) Mathf.Lerp(0, CAMERA_FOV_ADDITION, Mathf.InverseLerp(1,0,distance/_collider.radius));

        Time.timeScale = Mathf.Max(distance / _collider.radius, MIN_TIMESCALE);

        Character.GetCharacter().AttachedCameraController.SetAdditiveFov(fov);
        OnVortexDistanceUpdate(distance);
    }

    private void OnVortexDistanceUpdate(float distance)
    {
        if (distance < _entranceRadius) 
        {
            //if (SceneManager.GetSceneByName(_sceneName).IsValid())
                MakeTransition();
            //else {
            //    Debug.Log("Can't load scene. Probably wrong name in vortex settings or it is not included in build settings.");
            //}
        }
    }

    private void MakeTransition() 
    {
        if (_isEnter)
        {
            Countdown.Pause();
        }
        else 
        {
            Countdown.Unpause();
        }

        Time.timeScale = 1;
        SceneManager.LoadScene(_sceneName);
    }
}
