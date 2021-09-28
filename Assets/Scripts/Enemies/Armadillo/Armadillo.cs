using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static EnumMan;
using static UnityEngine.Random;

public class Armadillo : Enemy
{
    [SerializeField] private GameObject _playerObject;

    [SerializeField] private Animator _animator;

    private float _walkSpeed = 0.8f;
    private float _rotateSpeed = 20f;
    private float _rollSpeed = 600f;

    private float _minWalkTime = 3f;
    private float _maxWalkTime = 7f;
    private float _prepareSwipeTime = 1f;
    private float _prepareSwipeRotateSpeed = -50f;
    private float _swipeTime = 2f;
    private float _rollStartTime = 1f;
    private float _rollTime = 5f;
    private float _rollEndTime = 1f;
    private float _jumpTime = 2f;

    private Vector3 _targetPos;
    public Vector3 TargetPos => _targetPos;

    private BossState _previousState = BossState.Idle;
    private BossState _currentState = BossState.Idle;
    public BossState CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _previousState = _currentState;
            _currentState = value;
            _animator.SetInteger("BossState", (int) value);

            switch (value)
            {
                case BossState.Idle:
                    break;
                case BossState.Walk:
                    StartCoroutine(DelayStateChange(UnityEngine.Random.Range(_minWalkTime, _maxWalkTime), BossState.Decide_Attack));
                    break;
                case BossState.Decide_Attack:
                    CurrentState = DecideAttack();
                    break;
                case BossState.Prepare_Swipe:
                    StartCoroutine(DelayStateChange(_prepareSwipeTime, BossState.Swipe));
                    break;
                case BossState.Swipe:
                    StartCoroutine(DelayStateChange(_swipeTime, BossState.Walk));
                    break;
                case BossState.Roll_Start:
                    StartCoroutine(DelayStateChange(_rollStartTime, BossState.Roll));
                    break;
                case BossState.Roll:
                    StartCoroutine(DelayStateChange(_rollTime, BossState.Roll_End));
                    break;
                case BossState.Roll_End:
                    StartCoroutine(DelayStateChange(_rollEndTime, BossState.Walk));
                    break;
                case BossState.Jump:
                    StartCoroutine(DelayStateChange(_jumpTime, BossState.Walk));
                    break;
                default:
                    break;
            }
            StateChangedEvent.Invoke(_previousState, _currentState);
        }
    }

    // Events
    // previousState, currentState
    public event Action<BossState, BossState> StateChangedEvent = delegate { };

    protected void Start()
    {
        StateChangedEvent?.Invoke(_previousState, _currentState);
    }

    protected override void Move()
    {
        UpdateTargetPos();

        switch(CurrentState)
        {
            case BossState.Idle:
                CurrentState = BossState.Walk;
                break;
            case BossState.Walk:
                Walk(_walkSpeed);
                FacePlayer(_rotateSpeed);
                break;
            case BossState.Prepare_Swipe:
                FacePlayer(_prepareSwipeRotateSpeed);
                break;
            case BossState.Roll_Start:
                ;
                break;
            case BossState.Roll:
                Roll(_rollSpeed);
                break;
            case BossState.Roll_End:
                ;
                break;
            case BossState.Jump:
                ;
                break;
            default:
                break;
        }
    }

    private void UpdateTargetPos()
    {
        if (_playerObject != null)
        {
            _targetPos = _playerObject.transform.position;
        }

        // Update left/right animations
        bool playerToRight = MathFunctions.IsTargetToRight(Rb.position, _targetPos, transform.forward);
        _animator.SetBool("FaceRight", playerToRight);


        //_animator.SetInteger("BossState", (int)value);
    }

    // Walks towards player, up to speed
    private void Walk(float speed)
    {
        Vector3 currentPos = Rb.position;
        Vector3 newPos = currentPos + (_targetPos - currentPos).normalized * speed * Time.fixedDeltaTime;
        Rb.MovePosition(newPos);
    }

    // Rotates towards player, up to angle
    private void FacePlayer(float maxAngle)
    {
        Vector3 forward = transform.forward;
        if (maxAngle < 0)
            forward *= -1;

        float angle = MathFunctions.GetCappedAngle(Rb.position, _targetPos, forward, Mathf.Abs(maxAngle) * Time.fixedDeltaTime);

        // Rotate entire tower horizontally
        transform.Rotate(Vector3.up, angle);
    }

    private void Roll(float speed)
    {
        Vector3 currentPos = Rb.position;
        Vector3 dir = (_targetPos - currentPos).normalized;
        Vector3 rollForce = dir * speed * Time.fixedDeltaTime * Rb.mass;
        Rb.AddForce(rollForce);
    }

    private IEnumerator DelayStateChange(float time, BossState newState)
    {
        yield return new WaitForSeconds(time);
        CurrentState = newState;
    }

    private BossState DecideAttack()
    {
        BossState state;
        int choice = UnityEngine.Random.Range(0, 3);
        //choice = 2;
        switch (choice)
        {
            case 0:
                state = BossState.Prepare_Swipe;
                break;
            case 1:
                state = BossState.Roll_Start;
                break;
            case 2:
                state = BossState.Jump;
                break;
            default:
                Debug.Log("invalid choice");
                state = BossState.Prepare_Swipe;
                break;
        }
        return state;
    }
}
