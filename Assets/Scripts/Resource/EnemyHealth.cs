using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public GameObject deathAudio;
    override protected void Die()
    {
        PowersourceScript.enemyCount--;
        Instantiate(deathAudio);
        Destroy(gameObject);
    }
}
