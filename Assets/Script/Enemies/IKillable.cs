using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable<T>
{
    void Die();
    void EnemyTakeDamage(T damageTaken);

}