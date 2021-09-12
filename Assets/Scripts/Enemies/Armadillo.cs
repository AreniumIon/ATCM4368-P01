using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadillo : Enemy
{
    [SerializeField] private GameObject _playerObject;

    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _rotateSpeed = 2f;


    private Vector3 _targetPos;
    private BossState _currentState = BossState.Idle;


    enum BossState
    {
        Idle = 0,
        Walk = 1,
        Prepare_Swipe = 2,
        Swipe = 3,
        Jump = 4,
        Roll_Start = 5,
        Roll = 6,
        Roll_End = 7,
    }

    protected override void Move()
    {
        UpdateTargetPos();

        switch(_currentState)
        {
            case BossState.Idle:
                _currentState = BossState.Walk;
                break;
            case BossState.Walk:
                Walk(_walkSpeed);
                FacePlayer(_rotateSpeed);
                break;
        }
    }

    private void UpdateTargetPos()
    {
        if (_playerObject != null)
        {
            _targetPos = _playerObject.transform.position;
        }
    }

    // Walks towards player, up to speed
    private void Walk(float speed)
    {
        Vector3 currentPos = Rb.position;
        Vector3 newPos = currentPos + (_targetPos - currentPos).normalized * speed * Time.fixedDeltaTime;
        Rb.MovePosition(newPos);
    }

    // Rotates towards player, up to angle
    private void FacePlayer(float angle)
    {

    }
}
