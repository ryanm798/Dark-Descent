using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Resource
{
    public void Heal(float heal)
    {
        SetValue(currentValue + heal);
    }

    virtual public void Damage(float damage)
    {
        SetValue(currentValue - damage);
        if (currentValue == 0f)
        {
            Die();
        }
    }

    virtual protected void Die()
    {

    }
}
