using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniralResearchGame : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private OuterSpaceEnter _enter;
    [SerializeField] private float _comboDistance = 0.5f;
    [SerializeField] private ResearchVariantGenerator _generator;
    [SerializeField] private ResearchTurnDeadzone _deadzonePrefab;
    [SerializeField] private ResearchUiDrawer _drawer;
    private List<ResearchTurnDeadzone> _deadzones = new List<ResearchTurnDeadzone>();
    private Character _character;
    private Vector3 _currentTargetPostion;
    private int _currentTarget = 0;
    private int _currentAction = 0;
    private List<qtEvent> _currentEvents;
    private void OnEnable()
    {
        _enter.OnEnteted += CharacterEnteredCallback;
    }

    private void CharacterEnteredCallback(Character character)
    {
        character.GetComponent<CapsuleCollider>().enabled = false;
        StartGame(character);
    }

    private void StartGame(Character character)
    {
        _character = character;
        _character.LockMovement();
        _currentTargetPostion = GetWorldPosition(_currentTarget);
        _character.FlyTo(_currentTargetPostion);
        GenerateTriggers();
    }

    private void Finishgame()
    {

        _character.UnlockMovement();
        _character.StopFly();
        _deadzones.ForEach((ResearchTurnDeadzone deadzone) => { Destroy(deadzone); });
        _deadzones.Clear();

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
        _currentEvents = _generator.NewVariant();
        _drawer.DrawIcons(_currentEvents);
        for (int i = 0; i < _currentEvents.Count; i++) 
        {
            Debug.Log("You need to press " + _currentEvents[i].InputEvent);
            _currentEvents[i].InputEvent.performed += InputActionPerformed;
        }
        _currentEvents[0].InputEvent.Enable();
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
        _drawer.ClearIcons();
        _currentAction = 0;
        _currentTarget++;
        if (_currentTarget >= _lineRenderer.positionCount) { Finishgame(); return; }
        _currentTargetPostion = _lineRenderer.transform.TransformPoint(_lineRenderer.GetPosition(_currentTarget));
        _character.FlyTo(_currentTargetPostion);
    }
}
