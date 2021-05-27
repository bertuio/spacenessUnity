using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchVariantGenerator : MonoBehaviour
{
    [SerializeField] private int _elementsCount;
    [SerializeField] private List<qtEvent> _availableEvents;
    public List<qtEvent> NewVariant() 
    {
        List<qtEvent> events = new List<qtEvent>();

        for (int i = 0; i < _elementsCount; i++) 
        {
            events.Add(Instantiate(_availableEvents[Random.Range(0,_availableEvents.Count)]));            
        }

        return events;
    }
}
