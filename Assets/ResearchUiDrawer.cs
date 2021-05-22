using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchUiDrawer : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _imagePrefab;
    private int _extents = 100;
    private List<Image> _instantiatedImages = new List<Image>();
    private float _imageWidth;

    private void Awake()
    {
        _imageWidth = _imagePrefab.rectTransform.rect.width;
    }
    public void DrawIcons(List<qtEvent> events)
    {
        _extents = (int)(_imagePrefab.rectTransform.rect.width*(events.Count-1)/2f);
        for (int i = 0; i < events.Count; i++) 
        {
            Image image = Instantiate(_imagePrefab, Vector3.zero, Quaternion.identity, _canvas.transform);
            image.rectTransform.anchoredPosition = new Vector3(-_extents + _imageWidth * i, 0, 0);
            image.GetComponent<Image>().sprite = events[i].Image;
            _instantiatedImages.Add(image);
        }
    }
    public void ClearIcons() 
    {
        _instantiatedImages.ForEach((Image image)=>Destroy(image.gameObject));
        _instantiatedImages.Clear();
    }
}
