using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSound : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Kill");
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

}
