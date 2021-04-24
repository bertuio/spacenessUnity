using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame : MonoBehaviour
{
    protected Minigame() { }

    [SerializeField] protected MinigameCamera _minigameCamera;
    [SerializeField] protected MinigameInteractable _interactable;
    [SerializeField] private UnityEvent _onMinigameFinished;
    [SerializeField] private AudioSource _audioSucceed, _audioFailed;
    [SerializeField] private TutorialContainer _tutorial;
    [SerializeField] private bool _showTutorial;
    [SerializeField] private float _tutorialDelay;

    public virtual void StartGame()
    {

    }
    public virtual void InterruptGame()
    {
        DesetupPlayer();
    }
    public virtual void FinishGame()
    {
        DesetupPlayer();
        _onMinigameFinished?.Invoke();
        _interactable.OnInteractionEndedForced?.Invoke();
        if (_audioSucceed) _audioSucceed.Play();
    }
    public virtual void FailGame()
    {
        DesetupPlayer();
        _interactable.OnInteractionEndedForced?.Invoke();
        if (_audioFailed) _audioFailed.Play();
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
    }

    private void HandleInteractionDispatch() 
    {
        StartCoroutine(HandleInteraction());
    }

    private IEnumerator HandleInteraction()
    {
        SetupPlayer();
        if (_showTutorial)
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
