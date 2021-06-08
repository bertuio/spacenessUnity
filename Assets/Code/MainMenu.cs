using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        CustomCursor.ShowCursor();
    }
    public void StartGame() 
    {
        CustomCursor.HideCursor();
        SceneManager.LoadScene(2);
    }
}
