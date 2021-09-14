using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Armadillo : Enemy
{
    [SerializeField] private GameObject _playerObject;

    [SerializeField] private Animator _animator;

    private float _walkSpeed = 0.75f;
    private float _rotateSpeed = 20f;

    private float _minWalkTime = 3f;
    private float _maxWalkTime = 8f;
    private float _prepareSwipeTime = 1f;
    private float _prepareSwipeRotateSpeed = -60f;
    private float _swipeTime = 1f;

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


    public enum BossState
    {
        Idle = 0,
        Walk = 1,
        Decide_Attack = 2,
        Prepare_Swipe = 3,
        Swipe = 4,
        Jump = 5,
        Roll_Start = 6,
        Roll = 7,
        Roll_End = 8,
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
        bool playerToRight = true;


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
        Vector3 currentPos = Rb.position;

        Vector3 a = transform.forward;
        Vector3 b = (_targetPos - currentPos).normalized;

        // Horizontal
        Vector2 aHor = new Vector2(a.x, a.z);
        Vector2 bHor = new Vector2(b.x, b.z);

        float horAngle = Vector2.Angle(aHor, bHor);

        // Cap angle
        horAngle = Mathf.Clamp(horAngle, 0f, Mathf.Abs(maxAngle));

        // Left or right
        Vector3 cross = Vector3.Cross(aHor, bHor);
        if (cross.z > 0)
            horAngle *= -1;
        if (maxAngle < 0)
            horAngle *= -1;


        // Rotate entire tower horizontally
        transform.Rotate(Vector3.up, horAngle * Time.fixedDeltaTime);
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
