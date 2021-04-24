using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralResearchBoulder : MonoBehaviour
{
    private Vector3 _rotation;
    public string Id { get; private set; }
    private static string[] _prefixes = { "HK", "UN", "EPQ", "DRM", "ABR" };
    private void Awake()
    {
        Id = GenerateId();
        _rotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Debug.Log(Id);
    }

    private string GenerateId()
    {
        return _prefixes[Random.Range(0, _prefixes.Length)]+'-'+(Random.Range(0, 1000).ToString().PadLeft(4,'0'));
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
