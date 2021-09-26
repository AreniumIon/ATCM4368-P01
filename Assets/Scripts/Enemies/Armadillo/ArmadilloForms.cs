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

    bool isCurled = false;
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

        if (isCurled != shouldBeCurled)
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
    }

    private void RigidbodyUncurled(Rigidbody rb)
    {
        rb.mass = 100f;
        rb.constraints = FreezePositionY | FreezeRotationX | FreezeRotationZ;
    }

    private void ColliderUncurled(CapsuleCollider col)
    {
        col.height = 7;
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
    }

    private void RigidbodyCurled(Rigidbody rb)
    {
        rb.mass = 100f;
        rb.constraints = RigidbodyConstraints.None; // FreezePositionY;
    }

    private void ColliderCurled(CapsuleCollider col)
    {
        col.height = 0;
    }
}
