using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MiniralResearchGame : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private OuterSpaceEnter _enter;
    [SerializeField] private float _comboDistance = 0.5f;
    [SerializeField] private ResearchVariantGenerator _generator;
    [SerializeField] private ResearchTurnDeadzone _deadzonePrefab;
    [SerializeField] private ResearchUiDrawer _drawer;
    [SerializeField] private InputAction _anyKeyAction;
    [SerializeField] private AudioSource _audioGoingNext, _audioSucceed, _audioGoingBack;
    [SerializeField] private MineralResearchBoulderGrabber _grabber;
    [SerializeField] private UnityEvent _minigameSucceedCallback;
    [SerializeField] private List<string> retractionImmune;

    private List<ResearchTurnDeadzone> _deadzones = new List<ResearchTurnDeadzone>();
    private Character _character;
    private Vector3 _currentTargetPostion;
    private int _currentTarget = 0;
    private int _currentAction = 0;
    [SerializeField] private float _deltaTimer = 1f;
    private List<qtEvent> _currentEvents;
    [SerializeField] private ResearchTimer _timer;

    private void OnEnable()
    {
        _enter.OnEnteted += CharacterEnteredCallback;
        _anyKeyAction.started += CheckMissQte;
        _grabber.OnRareBoulderGot += _minigameSucceedCallback.Invoke;

        foreach (InputDevice device in InputSystem.devices)
        {
            foreach (InputControl control in device.allControls)
            {
                if (control.name == "anyKey") continue;
                if (device.name == "Mouse") if (control.name != "rightButton" & control.name != "leftButton") continue;
                if (retractionImmune.Contains(control.name)) { continue; }
                _anyKeyAction.AddBinding(control.path, "press");
            }
        }
    }

    private void OnDisable()
    {
        _timer.OnTimeExceeded -= GoBack;
        _enter.OnEnteted -= CharacterEnteredCallback;
        _anyKeyAction.started -= CheckMissQte;
        _grabber.OnRareBoulderGot -= _minigameSucceedCallback.Invoke;
        _grabber.Deactivate();
        _timer.Hide();
    }

    private void CheckMissQte(InputAction.CallbackContext context) 
    {
        Debug.Log("Check");
        if (context.control.path != _currentEvents[_currentAction].InputEvent.bindings[0].path.Replace("<", "/").Replace(">", ""))
        {
            GoBack();
        }
    }

    private void CharacterEnteredCallback(Character character)
    {
        character.GetComponent<CapsuleCollider>().enabled = false;
        StartGame(character);
    }

    private void StartGame(Character character)
    {
        _timer.OnTimeExceeded += GoBack;
        _character = character;
        _character.LockMovement();
        _currentTargetPostion = GetWorldPosition(_currentTarget);
        _character.FlyTo(_currentTargetPostion);
        GenerateTriggers();
        _grabber.Activate();
        _timer.Show();
    }

    private void Finishgame()
    {
        _timer.OnTimeExceeded -= GoBack;
        _anyKeyAction.Disable();
        _character.UnlockMovement();
        _character.StopFly();
        _deadzones.ForEach((ResearchTurnDeadzone deadzone) => { Destroy(deadzone); });
        _deadzones.Clear();
        _grabber.Deactivate();
        _timer.Hide();
    }

    private void GenerateTriggers()
    {
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            ResearchTurnDeadzone deadzone = Instantiate(_deadzonePrefab, GetWorldPosition(i), Quaternion.identity, transform);
            deadzone.onCharacterEntered += EnterDeadzoneCallback;
            _deadzones.Add(deadzone);
        }
    }

    private void EnterDeadzoneCallback()
    {
        _anyKeyAction.Enable();
        _currentEvents = _generator.NewVariant();
        _drawer.DrawIcons(_currentEvents);
        for (int i = 0; i < _currentEvents.Count; i++) 
        {
            Debug.Log("You need to press " + _currentEvents[i].InputEvent);
            _currentEvents[i].InputEvent.performed += InputActionPerformed;
        }
        _currentEvents[0].InputEvent.Enable();
        _timer.isPlaying = true;
    }

    private Vector3 GetWorldPosition(int point) 
    {
        return _lineRenderer.transform.TransformPoint(_lineRenderer.GetPosition(point));
    }

    private void InputActionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("You've pressed "+ _currentEvents[_currentAction]);
        _currentEvents[_currentAction].InputEvent.Disable();
        _currentEvents[_currentAction].InputEvent.performed-= InputActionPerformed;
        Debug.Log("IE disabled");
        _currentAction++;
        if (_currentAction >= _currentEvents.Count) {GoNext(); return; }
        _currentEvents[_currentAction].InputEvent.Enable();
    }

    private void GoNext()
    {
        _anyKeyAction.Disable();
        _drawer.ClearIcons();
        _currentAction = 0;
        _currentTarget++;
        if (_currentTarget >= _lineRenderer.positionCount)
        {
            Finishgame();
            _audioSucceed.Play();
            return;
        }
        _currentTargetPostion = _lineRenderer.transform.TransformPoint(_lineRenderer.GetPosition(_currentTarget));
        _character.FlyTo(_currentTargetPostion);
        _audioGoingNext.Play();
        _timer.timerMax -= _deltaTimer;
        _timer.Rewind();
        _timer.isPlaying = false;
    }
    private void GoBack()
    {
        Debug.Log("Rewound");
        _currentEvents[_currentAction].InputEvent.Disable();
        _drawer.ClearIcons();
        _currentAction = 0;
        if (_currentTarget <= 0)
        {
            Debug.Log("Branch");
            EnterDeadzoneCallback();
            _timer.Rewind();
            _timer.isPlaying = true;
        }
        else
        {
            _anyKeyAction.Disable();
            _currentTarget--;
            _currentTargetPostion = _lineRenderer.transform.TransformPoint(_lineRenderer.GetPosition(_currentTarget));
            _character.FlyTo(_currentTargetPostion);
            _timer.timerMax += _deltaTimer;
            _timer.Rewind();
            _timer.isPlaying = false;
        }
        _audioGoingBack.Play();
    }
}
