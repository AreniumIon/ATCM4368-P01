using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumMan;
using static UnityEngine.RigidbodyConstraints;

public class ArmadilloForms : MonoBehaviour
{
    private static List<BossState> uncurledStates = new List<BossState>()
    { 
        BossState.Decide_Attack,
        BossState.Idle,
        BossState.Jump,
        BossState.Prepare_Swipe,
        BossState.Roll_Start,
        BossState.Roll_End,
        BossState.Swipe,
        BossState.Walk,
    };

    private static List<BossState> curledStates = new List<BossState>()
    {
        BossState.Roll
    };

    bool _isCurled = false;
    public bool IsCurled => _isCurled;


    [SerializeField] GameObject uncurledObject;
    [SerializeField] GameObject curledObject;

    private void Start()
    {
        Armadillo armadillo = GetComponent<Armadillo>();
        armadillo.StateChangedEvent += CheckForm;
    }

    // Check for form change. Is subscribed to Armadillo.StateChangedEvent
    public void CheckForm(BossState previousState, BossState currentState)
    {
        bool shouldBeCurled = curledStates.Contains(currentState);

        if (_isCurled != shouldBeCurled)
        {
            if (shouldBeCurled)
                SetCurled();
            else
                SetUncurled();
        }
    }

    // Uncurled
    private void SetUncurled()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
            RigidbodyUncurled(rb);

        CapsuleCollider col = gameObject.GetComponent<CapsuleCollider>();
        if (col != null)
            ColliderUncurled(col);

        curledObject.SetActive(false);
        uncurledObject.SetActive(true);

        _isCurled = false;
    }

    private void RigidbodyUncurled(Rigidbody rb)
    {
        rb.mass = 100f;
        rb.constraints = FreezePositionY | FreezeRotationX | FreezeRotationZ;

        rb.velocity = new Vector3();
        rb.angularVelocity = new Vector3();
    }

    private void ColliderUncurled(CapsuleCollider col)
    {
        col.center = new Vector3(0f, .5f, .5f);
        col.radius = 2.5f;
        col.height = 7f;
        
        Transform tf = col.transform;
        tf.rotation = Quaternion.Euler(0f, GetAngleToPlayer(tf), 0f);
        tf.position = new Vector3(tf.position.x, 2, tf.position.z);
    }

    private float GetAngleToPlayer(Transform tf)
    {
        Armadillo a = GetComponent<Armadillo>();
        if (a != null)
        {
            float angle = MathFunctions.GetAngle(tf.position, a.TargetPos, Vector3.forward);
            return angle;
        }

        return 0f;
    }

    // Curled
    private void SetCurled()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
            RigidbodyCurled(rb);

        CapsuleCollider col = gameObject.GetComponent<CapsuleCollider>();
        if (col != null)
            ColliderCurled(col);

        uncurledObject.SetActive(false);
        curledObject.SetActive(true);

        _isCurled = true;
    }

    private void RigidbodyCurled(Rigidbody rb)
    {
        rb.mass = 100f;
        rb.constraints = RigidbodyConstraints.None; // FreezePositionY;
    }

    private void ColliderCurled(CapsuleCollider col)
    {
        col.center = new Vector3(0f, 1f, 0f);
        col.radius = 3f;
        col.height = 0f;

        Transform tf = col.transform;

        tf.rotation = Quaternion.Euler(0f, 0f, 0f);
        tf.position = new Vector3(tf.position.x, 2, tf.position.z);
    }
}
