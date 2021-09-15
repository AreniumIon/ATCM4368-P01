using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // Project instructions say "void TakeDamage", but returning bool is useful:
    // true: damage was taken
    // false: damage prevented (invincibility or other reason)
    public bool TakeDamage(int damage);
}
