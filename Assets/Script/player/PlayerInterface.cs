using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerInterface<T>
{
    void movement();

    void jumping();

    void Attack();

    void Die();

    void PlayerTakeDamage(T damageTaken);

}