using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VortexTransition : MonoBehaviour
{
    [SerializeField]
    private string scene;
    private Character character;
    private Vortex vortex;

    private void Start()
    {
        transform.parent.TryGetComponent(out vortex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out character))
        {
            vortex.Block = true;
            Time.timeScale = 1;
            SceneManager.LoadScene(scene);
        }
    }
}
