using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class EventLogDisplay : MonoBehaviour
{
    [SerializeField] private EventLogItem _prefab;
    private RectTransform _textParent;
    private List<EventLogItem> _eventList = new List<EventLogItem>();
    public static EventLogDisplay display;
    private void Awake()
    {
        if (display == null)
        {
            display = this;
        }
        else 
        {
            Destroy(gameObject);
        }
        _textParent = GetComponent<RectTransform>();
    }

    public void AddEvent(string str) 
    {
        EventLogItem item = Instantiate(_prefab, _textParent);
        item.onDestroy += () => { _eventList.Remove(item); };
        if (_eventList.Count > 0)
        {
            item.text.rectTransform.localPosition = new Vector3(0, _prefab.text.rectTransform.rect.height);
            item.text.rectTransform.localPosition += _eventList[_eventList.Count - 1].text.rectTransform.localPosition;
            item.CopyOffset(_eventList[_eventList.Count - 1]);
        }
        else
        {
            item.text.rectTransform.localPosition = new Vector3(_prefab.text.rectTransform.localPosition.x, _prefab.text.rectTransform.rect.height);
        }

        item.text.text = str;
        _eventList.Add(item);
        _eventList.ForEach((item) => { item.MoveDown(); });
    }
}
