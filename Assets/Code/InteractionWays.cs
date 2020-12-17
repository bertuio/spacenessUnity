using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public struct InteractionWays : IEnumerable
{

    [SerializeField] private InputActionMap _keys;
    [SerializeField] private List<InteractionWay> _list;

    public int Count => Mathf.Min(_keys.actions.Count, _list.Count);

    public void BindCallbacks() 
    {
        for (int i = 0; i < Count; i++)
        {
            GetWay(i).SetKey(_keys.actions[i], GetWay(i).Callback);
        }
    }

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return GetWay(i);
        }
    }

    public InteractionWay GetWay(int i) 
    {
        if (i < Count)
        {
            return _list[i];
        }
        else 
        {
            throw new IndexOutOfRangeException();
        }
    }
}
[Serializable]
public struct InteractionWay
{
    public void SetKey(InputAction inputAction, UnityEvent unityEvent) 
    {
        Key = inputAction;
        Path = inputAction.bindings[0].path;
        Debug.Log(pat());
        Key.performed += (InputAction.CallbackContext context) => unityEvent.Invoke();
    }

    public string pat() 
    {
        return Path;
    }

    [SerializeField] private string _description;
    public UnityEvent Callback;
    [NonSerialized] public InputAction Key;
    public string Path { get; private set; }
    public string Description => _description;
}