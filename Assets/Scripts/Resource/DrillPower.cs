using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPower : Health
{
    [Header("Drill Power Settings")]
    [Tooltip("Power units recharged per second")]
    public float rechargeRate = 0.1f;
    [Tooltip("Delay in seconds after taking damage before recharging")]
    public float rechargeDelay = 2.0f;
    private float timeOfLastHit;

    override protected void Start()
    {
        base.Start();
        timeOfLastHit = Time.time - rechargeDelay;
    }

    void Update()
    {
        if ( (currentValue != maxValue) && (Time.time - timeOfLastHit >= rechargeDelay) )
        {
            Heal(rechargeRate * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Damage(1f);
    }

    override public void Damage(float damage)
    {
        base.Damage(damage);
        timeOfLastHit = Time.time;
    }

    override protected void Die()
    {
        GameManager.Instance.Restart();
    }
}
