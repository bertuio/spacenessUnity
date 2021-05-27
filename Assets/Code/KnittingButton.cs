using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnittingButton : MonoBehaviour
{
    public System.Action OnClicked;
    public void Click() 
    {
        OnClicked?.Invoke();
        Destroy(gameObject);
    }
}
