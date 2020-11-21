using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private Text counter;
    public string counterValue { get; set; }

    private void Update()
    {
        counter.text = counterValue;
    }
}
