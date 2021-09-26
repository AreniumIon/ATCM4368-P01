using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumMan;
using static UnityEngine.Random;

public class Armadillo : Enemy
{
    [SerializeField] private GameObject _playerObject;

    [SerializeField] private Animator _animator;

    private float _walkSpeed = 0.8f;
    private float _rotateSpeed = 20f;

    private float _minWalkTime = 3f;
    private float _maxWalkTime = 8f;
    private float _prepareSwipeTime = 1f;
    private float _prepareSwipeRotateSpeed = -60f;
    private float _swipeTime = 2f;

    private Vector3 _targetPos;

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
                    StartCoroutine(DelayStateChange(Random.Range(_minWalkTime, _maxWalkTime), BossState.Decide_Attack));
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
                default:
                    break;
            }
        }
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
        float angle = MathFunctions.GetCappedAngle(Rb.position, _targetPos, transform.forward, maxAngle);

        // Rotate entire tower horizontally
        transform.Rotate(Vector3.up, angle * Time.fixedDeltaTime);
    }

    private IEnumerator DelayStateChange(float time, BossState newState)
    {
        yield return new WaitForSeconds(time);
        CurrentState = newState;
    }

    private BossState DecideAttack()
    {
        return BossState.Prepare_Swipe;
    }
}
