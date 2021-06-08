using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Minigame : MonoBehaviour
{
    [SerializeField] protected MinigameCamera _minigameCamera;
    [SerializeField] protected MinigameInteractable _interactable;
    [SerializeField] private UnityEvent _onMinigameFinished;
    [SerializeField] private AudioClip _audioSucceed, _audioFailed;
    [SerializeField] private TutorialContainer _tutorial;
    [SerializeField] private bool _showTutorial;
    [SerializeField] private float _tutorialDelay;

    private AudioSource _audioEmitter;

    protected virtual string MinigameName { get; set; } = "minigame";

    protected void EmitSound(AudioClip clip) 
    {
        _audioEmitter.clip = clip;
        _audioEmitter.Play();
    }
    public virtual void StartGame()
    {
        EventLogDisplay.display.AddEvent($"Activating {MinigameName}");
    }
    public virtual void InterruptGame()
    {
        EventLogDisplay.display.AddEvent($"Aborting {MinigameName}");
        DesetupPlayer();
    }
    public virtual void FinishGame()
    {
        EventLogDisplay.display.AddEvent($"{MinigameName.ToUpper()} succeed");
        Countdown.SpeedDown();
        DesetupPlayer();
        _onMinigameFinished?.Invoke();
        _interactable.OnInteractionEndedForced?.Invoke();
        if (_audioSucceed) EmitSound(_audioSucceed);
    }
    public virtual void FailGame()
    {
        EventLogDisplay.display.AddEvent($"Failing {MinigameName} ...");
        DesetupPlayer();
        _interactable.OnInteractionEndedForced?.Invoke();
        if (_audioFailed) EmitSound(_audioFailed);
    }

    private void SetupPlayer()
    {
        CustomCursor.ShowCursor();
        CustomCursor.Mute();
        FindObjectOfType<Character>().LockMovementAndRotation();
        Debug.LogError(Camera.main.gameObject.GetComponent<CameraController>());
        Debug.LogError(_minigameCamera);
        Camera.main.gameObject.GetComponent<CameraController>().SimulateCamera(_minigameCamera);
    }

    private void DesetupPlayer()
    {
        CustomCursor.HideCursor();
        Camera.main.GetComponent<CameraController>().ForceChasing();
        FindObjectOfType<Character>().UnlockMovement();
    }

    public void Awake()
    {
        _interactable.OnInteracted += HandleInteractionDispatch;
        _interactable.OnInteractionEnded += InterruptGame;
        _audioEmitter = GetComponent<AudioSource>();
    }

    private void HandleInteractionDispatch() 
    {
        StartCoroutine(HandleInteraction());
    }

    private IEnumerator HandleInteraction()
    {
        SetupPlayer();
        if (_showTutorial && _tutorial)
        {
            _tutorial.Show();
            EventLogDisplay.display.AddEvent("Educating ...");
            yield return new WaitForSeconds(_tutorialDelay);
            _showTutorial = false;
            _tutorial.Hide();
            yield return new WaitForSeconds(1f);
        }
        StartGame();
    }
}
