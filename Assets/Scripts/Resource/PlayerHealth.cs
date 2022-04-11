using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    override protected void Die()
    {
        GameManager.Instance.Restart();
    }
}
