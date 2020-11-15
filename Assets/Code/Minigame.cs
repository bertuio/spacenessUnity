using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Minigame))]
public abstract class Minigame : MonoBehaviour
{
    public MinigameCamera minigameCameraTransform;
    public CameraController charachterCameraController { get; private set; }
    private Character _enteredCharacter;
    private MinigameServiceKeyboardHandler bufferZoneKeyboardHandler;
    private MinigameServiceKeyboardHandler minigameStartedKeyboardHandler;
    private MinigameServiceKeyboardHandler currentKeyboardHandler;
    private MinigameServiceKeyboardHandler outerZoneKeyboardHandler;
    private GUIStyle style = new GUIStyle();
    public virtual void CameraBehaviour()
    {
        Transform cameraTransform = charachterCameraController.transform;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, minigameCameraTransform.transform.position, charachterCameraController.LinearSmoothRate/2);
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, minigameCameraTransform.transform.rotation, charachterCameraController.AngularSmoothRate/2);
    }

    private void Start()
    {
        style.normal.textColor = new Color(0, 0, 0);
        style.alignment = TextAnchor.LowerCenter;
        style.fontSize = 25;

        minigameStartedKeyboardHandler = new MinigameServiceKeyboardHandler("Escape", ExitGame, "Press Esc to finish", style);
        bufferZoneKeyboardHandler = new MinigameServiceKeyboardHandler("Space", InitializeGame, "Press Space to start", style);
        outerZoneKeyboardHandler = new MinigameServiceKeyboardHandler("", delegate { });
        currentKeyboardHandler = outerZoneKeyboardHandler;
    }
    private void triggerZoneEventHandle(Collider other, MinigameServiceKeyboardHandler handler)
    {

        if (other.TryGetComponent(out _enteredCharacter))
        {
            SetUpEnteredCharacter();
            OnEnteredAreaEvent(_enteredCharacter);
            currentKeyboardHandler = handler;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerZoneEventHandle(other, bufferZoneKeyboardHandler);
    }

    private void OnTriggerExit(Collider other)
    {
        triggerZoneEventHandle(other, outerZoneKeyboardHandler);
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
        private string hint;
        private GUIStyle style;
        public Action UpdateHandler;
        public MinigameServiceKeyboardHandler(string stringEvent, Action callback, string hint, GUIStyle style) 
        {
            this.callback = callback;
            keyboardEvent = Event.KeyboardEvent(stringEvent);
            this.hint = hint;
            this.style = style;
            UpdateHandler = UpdateHandlerWithHint;
        }
        public MinigameServiceKeyboardHandler(string stringEvent, Action callback)
        {
            this.callback = callback;
            keyboardEvent = Event.KeyboardEvent(stringEvent);
            UpdateHandler = UpdateHandlerWithoutHint;
        }

        private void UpdateHandlerWithoutHint()
        {
            if (Event.current.Equals(keyboardEvent))
            {
                callback();
            }
        }
        private void UpdateHandlerWithHint() 
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 200, 50), hint, style);
            UpdateHandlerWithoutHint();
        }
    }
}
