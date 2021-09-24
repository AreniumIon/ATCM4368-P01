using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArmadilloForms
{
    
    public static void SetUncurled(GameObject armadillo)
    {
        Rigidbody rb = armadillo.GetComponent<Rigidbody>();
        if (rb != null)
            RigidbodyUncurled(rb);

        CapsuleCollider col = armadillo.GetComponent<CapsuleCollider>();
        if (col != null)
            ColliderUncurled(col);
    }

    private static void RigidbodyUncurled(Rigidbody rb)
    {

    }

    private static void ColliderUncurled(CapsuleCollider col)
    {

    }


    public static void SetCurled(GameObject armadillo)
    {
        Rigidbody rb = armadillo.GetComponent<Rigidbody>();
        if (rb != null)
            RigidbodyCurled(rb);

        CapsuleCollider col = armadillo.GetComponent<CapsuleCollider>();
        if (col != null)
            ColliderCurled(col);
    }

    public static void RigidbodyCurled(Rigidbody rb)
    {

    }

    private static void ColliderCurled(CapsuleCollider col)
    {

    }
}
