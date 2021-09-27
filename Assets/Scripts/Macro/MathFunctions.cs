using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathFunctions
{
    public static bool IsTargetToRight(Vector3 basePos, Vector3 targetPos, Vector3 forward)
    {
        float angle = GetAngle(basePos, targetPos, forward);

        return angle > 0;
    }

    public static float GetAngle(Vector3 basePos, Vector3 targetPos, Vector3 forward)
    {
        Vector3 dir2 = (targetPos - basePos).normalized;

        // Horizontal
        Vector2 a = new Vector2(forward.x, forward.z);
        Vector2 b = new Vector2(dir2.x, dir2.z);

        return GetAngle(a, b);
    }

    public static float GetAngle(Vector2 dir1, Vector2 dir2)
    {
        float angle = Vector2.Angle(dir1, dir2);

        // Left or right
        Vector3 cross = Vector3.Cross(dir1, dir2);
        if (cross.z > 0)
            angle *= -1;

        return angle;
    }

    public static float GetCappedAngle(Vector3 basePos, Vector3 targetPos, Vector3 forward, float maxAngle)
    {
        Vector3 dir2 = (targetPos - basePos).normalized;

        // Horizontal
        Vector2 a = new Vector2(forward.x, forward.z);
        Vector2 b = new Vector2(dir2.x, dir2.z);

        return GetCappedAngle(a, b, maxAngle);
    }

    public static float GetCappedAngle(Vector2 dir1, Vector2 dir2, float maxAngle)
    {
        float angle = Vector2.Angle(dir1, dir2);

        // Cap angle
        angle = Mathf.Clamp(angle, 0f, Mathf.Abs(maxAngle));

        // Left or right
        Vector3 cross = Vector3.Cross(dir1, dir2);
        if (cross.z > 0)
            angle *= -1;
        if (maxAngle < 0)
            angle *= -1;

        return angle;
    }

    public static bool IsMatchingLayer(LayerMask lm, int layer)
    {
        return lm == (lm | (1 << layer));
    }
}
