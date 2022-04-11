using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteScript : MonoBehaviour
{
    EnemyHealth health;
    GameObject player;
    public float damage;
    public float attackCooldown = 1.0f;
    float timeColliding = 0f;
    public float speed = 1f;

    AudioSource AS1; //BRUTE OUCH
    AudioSource AS2; //BRUTE WALK
    bool makeSound = false;


    private void Start()
    {
        health = GetComponent<EnemyHealth>();

        int rand = Random.Range(0, 361);
        float x = 4.0f * Mathf.Cos(rand * Mathf.Deg2Rad);
        float y = 4.0f * Mathf.Sin(rand * Mathf.Deg2Rad);
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        transform.position = new Vector3(x, y, 0.0f);

        player = GameObject.FindWithTag("Player");

        //transform.LookAt(powersource.transform);

        Vector3 offset = player.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(
                               Vector3.forward, // Keep z+ pointing straight into the screen.
                               offset           // Point y+ toward the target.
                             );

        AudioSource[] audios = GetComponents<AudioSource>();
        AS1 = audios[0];
        AS2 = audios[1];
        StartCoroutine("SoundCooldown");
    }

    private void Update()
    {

        if (makeSound)
        {
            AS2.Play();
            makeSound = false;
            StartCoroutine("SoundCooldown");
        }

        Vector3 offset = player.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(
                               Vector3.forward, // Keep z+ pointing straight into the screen.
                               offset           // Point y+ toward the target.
                             );
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().TakeDamage(damage);
            timeColliding = 0f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timeColliding += Time.deltaTime;
            if (timeColliding > attackCooldown)
            {
                collision.gameObject.GetComponent<PlayerScript>().TakeDamage(damage);
                timeColliding = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerMelee")
        {
            health.Damage(collision.gameObject.GetComponent<PlayerMeleeScript>().damage);
            AS1.Play();
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            health.Damage(collision.gameObject.GetComponent<PlayerRangedScript>().damage);
            AS1.Play();
        }
    }

    IEnumerator SoundCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        makeSound = true;
    }

}
