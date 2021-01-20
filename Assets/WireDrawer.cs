using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireDrawer : MonoBehaviour
{

    private LineRenderer _renderer;
    private float _wireWidth = 0.004f;
    private float _wireHeight = 0.02f;
    private List<LineRenderer> _wires = new List<LineRenderer>();
    [SerializeField] private Material _wireMaterial;
    public void ReplaceEnd(Transform endTransform)
    {
        _renderer.SetPosition(1, endTransform.position + endTransform.TransformVector(Vector3.up * _wireHeight));
    }
    public void ReplaceEnd(Vector3 position, Transform planeTransform)
    {
        _renderer.SetPosition(1, position + planeTransform.TransformVector(Vector3.up * _wireHeight));
    }

    public void DestroyLast() 
    {
        Destroy(_renderer.gameObject);
        _wires.RemoveAt(_wires.Count-1);
    }

    public void Flush() 
    {
        foreach (LineRenderer renderer in _wires) 
        {
            Destroy(renderer.gameObject);
        }
        _wires.Clear();
    }
    public void SetupNewLineRenderer(Transform startTransform)
    {
        _renderer = (new GameObject("line")).AddComponent<LineRenderer>();
        _wires.Add(_renderer);
        _renderer.positionCount = 2;
        _renderer.useWorldSpace = true;
        _renderer.startWidth = _wireWidth;
        _renderer.endWidth = _wireWidth;
        _renderer.SetPosition(0, startTransform.position + startTransform.TransformVector(Vector3.up * _wireHeight));
        _renderer.SetPosition(1, startTransform.position + startTransform.TransformVector(Vector3.up * _wireHeight));
        _renderer.material = _wireMaterial;
    }
}
