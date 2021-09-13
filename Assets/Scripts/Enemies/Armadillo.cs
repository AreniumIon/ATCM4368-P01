using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadillo : Enemy
{
    [SerializeField] private GameObject _playerObject;

    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private float _rotateSpeed = 30f;


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
        Vector3 currentPos = Rb.position;

        Vector3 a = transform.forward;
        Vector3 b = (_targetPos - currentPos).normalized;

        // Horizontal
        Vector2 aHor = new Vector2(a.x, a.z);
        Vector2 bHor = new Vector2(b.x, b.z);

        float horAngle = Vector2.Angle(aHor, bHor);

        // Left or right
        Vector3 cross = Vector3.Cross(aHor, bHor);
        if (cross.z > 0)
            horAngle *= -1;

        // Rotate entire tower horizontally
        transform.Rotate(Vector3.up, horAngle * Time.fixedDeltaTime);
    }
}
