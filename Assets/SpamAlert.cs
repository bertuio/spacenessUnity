using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamAlert : MonoBehaviour
{
    public void Delay(int delay) 
    {
        Destroy(gameObject, delay * (1 + Random.Range(-.2f, .8f)));
    }
}
