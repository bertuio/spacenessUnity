using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamAlert : MonoBehaviour
{
    [SerializeField] private AudioSource _appearedAudio;
    public System.Action PreDestroyed;
    public void Delay(int delay) 
    {
        Destroy(gameObject, delay * (1 + Random.Range(-.2f, .7f)));
    }

    private void Awake()
    {
        _appearedAudio.Play();
    }

    private void OnDestroy()
    {
        PreDestroyed?.Invoke();
    }
}
