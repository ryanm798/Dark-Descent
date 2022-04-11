using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowersourceScript : MonoBehaviour
{
    
    public GameObject PLAYER;
    public GameObject RAT;
    public GameObject BRUTE;
    DrillPower HEALTH;
    public static int enemyCount;

    private void Start()
    {
        HEALTH = gameObject.GetComponent<DrillPower>();
        enemyCount = 0;
    }

    public void SpawnRat(int _quantity)
    {
        for (int i = 0; i < _quantity; i++)
        {
            GameObject ratObj = Instantiate(RAT, transform.position, transform.rotation);
            ratObj.transform.parent = gameObject.transform.parent;
            //ratObj.transform.localScale = new Vector3(0.05f, 0.05f, 1.0f);
        }
        enemyCount += _quantity;
    }

    public void SpawnBrute(int _quantity)
    {
        for (int i = 0; i < _quantity; i++)
        {
            GameObject bruteObj = Instantiate(BRUTE, transform.position, transform.rotation);
            bruteObj.transform.parent = gameObject.transform.parent;
            //bruteObj.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
        }
        enemyCount += _quantity;
    }

    public void TakeDamage(float _val)
    {
        HEALTH.Damage(_val);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerMelee")
        {
            Destroy(gameObject);
        }
    }

}
