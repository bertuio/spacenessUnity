using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamSpawner : MonoBehaviour
{
    [SerializeField] private Canvas _parent;
    [SerializeField] private SpamItem _itemPrefab;
    [SerializeField] private SpamAlert _alertPrefab;
    [SerializeField] private AudioSource _disappearedAudio;
    private List<SpamItem> _items = new List<SpamItem>();
    public System.Action OnWinCondition;
    private float _canvasExtent;

    private void Awake()
    {
        _canvasExtent = _parent.GetComponent<RectTransform>().rect.width/2;
    }

    public void Spawn(int count, int delay, int maxAlerts) 
    {
        for (int i = 0; i < count; i++)
        {
            SpamItem item = Instantiate(_itemPrefab, _parent.transform);
            item.activated += delegate 
                {
                    CheckWinCondition();
                    int counts = Random.Range(1, maxAlerts+1);
                    for (int j = 0; j < count; j++)
                    {
                        SpamAlert alert = Instantiate(_alertPrefab, _parent.transform);
                        alert.GetComponent<RectTransform>().anchoredPosition = new Vector3(_canvasExtent * Random.Range(-.9f, .9f), _canvasExtent * Random.Range(-.9f, .9f), 0.1f);
                        alert.PreDestroyed += _disappearedAudio.Play;
                        alert.Delay(delay);
                    }
                };
            item.GetComponent<RectTransform>().anchoredPosition = new Vector3(_canvasExtent * Random.Range(-.9f, .9f), _canvasExtent * Random.Range(-.9f, .9f), 0.1f);
            _items.Add(item);
        }
    }

    private void CheckWinCondition() 
    {
        if (_items.TrueForAll((SpamItem item) => { return item.Toggled; })) 
        {
            OnWinCondition?.Invoke();
        }
    }

    public void Flush() 
    {
        foreach (SpamItem obj in _items) 
        {
            Destroy(obj.gameObject);
        }

        _items.Clear();
    }
}
