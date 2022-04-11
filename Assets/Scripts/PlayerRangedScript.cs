using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedScript : MonoBehaviour
{

    public float SPEED;
    public float damage = 10;
    public static float maxDamage = 10;

    public void Awake()
    {
        StartCoroutine("Duration");
    }
    void Update()
    {
        transform.position += transform.up * SPEED * Time.deltaTime;
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }
}
