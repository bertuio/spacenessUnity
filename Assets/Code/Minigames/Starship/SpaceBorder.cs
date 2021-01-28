using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBorder : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Ship ship)) 
        {
            ship.Crush();
        }
    }
}
