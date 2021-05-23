using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class EventLogItem : MonoBehaviour
{
    private static float _messageSpeed = .07f;
    private static float _messageLifetime = 5;
    public TMP_Text text;
    private Vector3 _targetOffset;
    public event System.Action onDestroy;
    private void Awake()
    {
        StartCoroutine(Kill());
    }
    public void CopyOffset(EventLogItem item)
    {
        _targetOffset = item._targetOffset;
    }
    public void MoveDown() 
    {
        _targetOffset -= Vector3.up * text.rectTransform.rect.height;
    }
    private void FixedUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition+_targetOffset, _messageSpeed);
        _targetOffset = Vector3.Lerp(_targetOffset, Vector3.zero, _messageSpeed);
    }

    private IEnumerator Kill() 
    {
        yield return new WaitForSeconds(_messageLifetime);
        onDestroy?.Invoke();
        _targetOffset += Vector3.left * 1100;
        yield return new WaitUntil(() => _targetOffset.magnitude<10);
        Destroy(gameObject);
    }
}
