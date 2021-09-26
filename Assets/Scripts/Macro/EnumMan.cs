using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumMan
{
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
}
