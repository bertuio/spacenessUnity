using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CapsuleCollider))]
public class Vortex : MonoBehaviour
{
    private GameObject characterGo;
    private Character character;
    [SerializeField]
    private Transform vortexBegin, vortexEnd;
    public bool Block { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out character)) 
        {
            characterGo = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out character))
        {
            Time.timeScale = 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (character && !Block && other.gameObject == characterGo) 
        {
            Time.timeScale = Mathf.Max(Mathf.Pow(Mathf.InverseLerp(vortexEnd.localPosition.z, vortexBegin.localPosition.z, transform.InverseTransformPoint(character.transform.position).z),3), 0.1f);
        }
    }
}
