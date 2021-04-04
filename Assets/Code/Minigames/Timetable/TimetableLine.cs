using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimetableLine : MonoBehaviour
{
    private static string _textureId = "Texture2D_8e89e9fd8e5446abbbf8fd0082bed725";
    public MeshFilter ExtentsFilter;
    [SerializeField] private Camera _renderCamera;
    [SerializeField] private GameObject _target;
    [SerializeField] private Transform _renderingScene;
    [SerializeField] private MeshRenderer _backplaneRenderer;
    [SerializeField] private Material _dislightenMaterial, _enlightenMaterial;
    [SerializeField] private Animation _appearingAnimation;
    private void Start()
    {
        RenderTexture rt;
        rt = new RenderTexture(128, 128, 2, RenderTextureFormat.Default);
        rt.Create();
        _renderCamera.targetTexture = rt;
        _target.GetComponent<MeshRenderer>().material.SetTexture(_textureId, rt);
        _renderingScene.position += new Vector3(UnityEngine.Random.Range(-1000,1000),0, UnityEngine.Random.Range(-1000,1000));
    }

    public void Enlight() 
    {
        _backplaneRenderer.material = _enlightenMaterial;
    }
    public void Dislight() 
    {
        _backplaneRenderer.material = _dislightenMaterial;
    }

    [SerializeField] private Text _text;
    public string Value { get; private set; }

    public void SetText(string text) 
    {
        Value = text;
        _text.text = Value;
        _appearingAnimation.Play();
    }

    public int Index { get; set; } = -1;

}
