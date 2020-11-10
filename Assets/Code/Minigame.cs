using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Minigame))]
public abstract class Minigame : MonoBehaviour
{
    public CameraController charachterCameraController { get; private set; }
    private Character _enteredCharacter;
    private GameObject headbone;
    private MinigameServiceKeyboardHandler bufferZoneKeyboardHandler;
    private MinigameServiceKeyboardHandler minigameStartedKeyboardHandler;
    private MinigameServiceKeyboardHandler currentKeyboardHandler;
    private MinigameServiceKeyboardHandler outerZoneKeyboardHandler;
    private GUIStyle style = new GUIStyle();
    public virtual void CameraBehaviour()
    {
        charachterCameraController.transform.SetPositionAndRotation(headbone.transform.position, headbone.transform.rotation);
    }

    private void Start()
    {
        style.normal.textColor = new Color(0, 0, 0);
        style.alignment = TextAnchor.LowerCenter;
        style.fontSize = 22;

        minigameStartedKeyboardHandler = new MinigameServiceKeyboardHandler("Escape", ExitGame, PrintHintEnd);
        bufferZoneKeyboardHandler = new MinigameServiceKeyboardHandler("Space", InitializeGame, PrintHintStart);
        outerZoneKeyboardHandler = new MinigameServiceKeyboardHandler("", delegate { });
        currentKeyboardHandler = outerZoneKeyboardHandler;
    }
    //Исправить дублирование
    private void PrintHintStart()
    {
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 200, 50), "Press Space to start", style);
    }
    private void PrintHintEnd()
    {
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 200, 50), "Press Esc to finish", style);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _enteredCharacter))
        {
            //UI
            SetUpEnteredCharacter();
            OnEnteredAreaEvent(_enteredCharacter);
            currentKeyboardHandler = bufferZoneKeyboardHandler;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out _enteredCharacter))
        {
            //UI
            SetUpEnteredCharacter();
            OnEnteredAreaEvent(_enteredCharacter);
            currentKeyboardHandler = outerZoneKeyboardHandler;
        }
    }

    private void ExitGame() 
    {
        currentKeyboardHandler = bufferZoneKeyboardHandler;
        charachterCameraController.ForceChasing();
        _enteredCharacter.SetMovementLock(false);
    }

    private void InitializeGame()
    {
        currentKeyboardHandler = minigameStartedKeyboardHandler;
        SetUpStartedCharacter();
        StartGame();
    }

    private void OnGUI()
    {
        currentKeyboardHandler.UpdateHandler();
    }

    private void SetUpEnteredCharacter()
    {
        headbone = _enteredCharacter.Head;
        charachterCameraController = _enteredCharacter.AttachedCameraController;
    }

    private void SetUpStartedCharacter() 
    {
        charachterCameraController?.SetCameraMinigame(this);
        _enteredCharacter.SetMovementLock(true);
    }

    public virtual void OnEnteredAreaEvent(Character character) 
    {
    
    }

    public abstract void StartGame();

    private class MinigameServiceKeyboardHandler
    {
        private Action callback;
        private Event keyboardEvent;
        private Action handlerAction;
        public MinigameServiceKeyboardHandler(string stringEvent, Action callback, Action handlerAction) 
        {
            this.callback = callback;
            keyboardEvent = Event.KeyboardEvent(stringEvent);
            this.handlerAction = handlerAction;
        }
        public MinigameServiceKeyboardHandler(string stringEvent, Action callback)
        {
            this.callback = callback;
            keyboardEvent = Event.KeyboardEvent(stringEvent);
        }

        public void UpdateHandler()
        {
            handlerAction?.Invoke();
            if (Event.current.Equals(keyboardEvent))
            {
                callback();
            }
        }
    }
}
