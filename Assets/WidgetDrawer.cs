using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class WidgetDrawer : MonoBehaviour
{
    [SerializeField] private RectTransform _widget;
    [SerializeField] private float _scaleTune;
    private RectTransform _transform;
    private MineralResearchBoulder _assignedBoulder;


    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public void UpdateBoulder(MineralResearchBoulder boulder)
    {
        _assignedBoulder = boulder;
    }

    private void Update()
    {
        if (_assignedBoulder) {
            Vector3 viewportCoordinates = Camera.main.WorldToViewportPoint(_assignedBoulder.transform.position);
            Vector3 canvasCoordinates = viewportCoordinates * _transform.rect.size - _transform.rect.size / 2;
            _widget.localPosition = canvasCoordinates;
            _widget.localScale = Vector3.one * _scaleTune / viewportCoordinates.z * _assignedBoulder.transform.lossyScale.x;
        }
    }

    public void Show() 
    {
        _widget.gameObject.SetActive(true);
    }
    public void Hide()
    {
        _widget.gameObject.SetActive(false);
    }
}
