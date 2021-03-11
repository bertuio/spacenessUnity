using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralResearchBoulder : MonoBehaviour
{
    private Vector3 _rotation;
    private void Awake()
    {
        _rotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    public void Grab() 
    {
        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        transform.Rotate(_rotation, UnityEngine.Space.World);
    }
}
