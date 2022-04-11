using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeScript : MonoBehaviour
{
    public float damage = 5;
    
    public void Awake()
    {
        StartCoroutine("Duration");
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }
}
