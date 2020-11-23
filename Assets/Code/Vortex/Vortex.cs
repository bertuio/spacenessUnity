using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class Vortex : MonoBehaviour
{
    [SerializeField]
    private string scene;
    private GameObject characterGo;
    private Character character;
    [SerializeField]
    private bool enterence;
    public bool Block { get; set; }

    [SerializeField]
    private SphereCollider slowerer;

    private Vector3 slowererCenter;
    private float slowererRadius;
    private float ratio;

    private void Start()
    {
        slowererCenter = transform.position;
        slowererRadius = slowerer.radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out character)) 
        {
            characterGo = other.gameObject;
        }
    }

    public void Teleport() 
    {
        Block = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
        if (enterence) FindObjectOfType<Countdown>()?.Pause();
        else FindObjectOfType<Countdown>()?.Resume();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out character))
        {
            UpdateTimescale(1);
            UpdateFov(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (character && !Block && other.gameObject == characterGo)
        {
            UpdateRatio(other.transform);
            UpdateTimescale(ratio);
            UpdateFov(ratio);
        }
    }
    private void UpdateRatio(Transform other) 
    {
        ratio = Vector3.Distance(slowererCenter, other.transform.position) / slowererRadius;
    }
    private void UpdateTimescale(float t)
    {
        Time.timeScale = Mathf.Clamp(1 / (1 + Mathf.Exp(-7f * (t - 0.7f))), 0.05f, 1f);
    }
    private void UpdateFov(float t)
    {
        character.AttachedCameraController.VortexAffect(Mathf.Clamp(t, 0.05f, 1f));
    }
}
