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

    protected void EmitSound(AudioClip clip) 
    {
        _audioEmitter.clip = clip;
        _audioEmitter.Play();
    }
    public virtual void StartGame()
    {
        
    }
    public virtual void InterruptGame()
    {
        DesetupPlayer();
    }
    public virtual void FinishGame()
    {
        Countdown.SpeedDown();
        DesetupPlayer();
        _onMinigameFinished?.Invoke();
        _interactable.OnInteractionEndedForced?.Invoke();
        if (_audioSucceed) EmitSound(_audioSucceed);
    }
    public virtual void FailGame()
    {
        DesetupPlayer();
        _interactable.OnInteractionEndedForced?.Invoke();
        if (_audioFailed) EmitSound(_audioFailed);
    }

    private void SetupPlayer() 
    {
        FindObjectOfType<Character>().LockMovementAndRotation();
        Camera.main.gameObject.GetComponent<CameraController>().SimulateCamera(_minigameCamera);
    }

    private void DesetupPlayer() 
    {
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
            yield return new WaitForSeconds(_tutorialDelay);
            _showTutorial = false;
            _tutorial.Hide();
            yield return new WaitForSeconds(1f);
        }
        StartGame();
    }
}
