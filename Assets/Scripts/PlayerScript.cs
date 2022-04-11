using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{

    float playerXMomentum; //PLAYERS ANTICIPATED MOVEMENT IN X DIRECTION
    float playerYMomentum; //PLAYERS ANTICIPATED MOVEMENT IN Y DIRECTION
    public float moveSpeed; //SPEED AT WHICH PLAYER WILL MOVE IN X/Y DIRECTIONS
    PlayerHealth HEALTH;


    Rigidbody2D rb;
    public GameObject powersource;
    public GameObject meleeattack;
    public GameObject rangedattack;
    AudioSource rangedSfx = null;


    bool meleeCD;

    float gunCharge;
    public float gunChargeSpeed = 5;
    public float minCharge = 2;
    public Color chargeColor;
    SpriteRenderer spriteRenderer;
    Color defaultPlayerColor;
    public Light2D playerLight;
    Color defaultLightColor;
    public float maxLightIntensity = 0.7f;
    float defaultLightIntensity;
    public float maxBulletIntensity = 0.9f;
    public float minBulletIntensity = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        //HEALTH_BAR_SLIDER = HEALTH_BAR.GetComponent<Slider>();
        playerXMomentum = 0.0f;
        playerYMomentum = 0.0f;

        rb = gameObject.GetComponent<Rigidbody2D>();
        HEALTH = gameObject.GetComponent<PlayerHealth>();

        meleeCD = false;
        gunCharge = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultPlayerColor = spriteRenderer.color;
        defaultLightColor = playerLight.color;
        defaultLightIntensity = playerLight.intensity;
        maxLightIntensity = Mathf.Max(maxLightIntensity, defaultLightIntensity);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        Vector3 offset = mousePosition - transform.position;

        transform.rotation = Quaternion.LookRotation(
                               Vector3.forward, // Keep z+ pointing straight into the screen.
                               offset           // Point y+ toward the target.
                             );


        playerXMomentum = 0.0f;
        playerYMomentum = 0.0f;
        
        if (Input.GetButton("Up"))
        {
            playerYMomentum = moveSpeed;
        } else if (Input.GetButton("Down"))
        {
            playerYMomentum = -moveSpeed;
        }

        if (Input.GetButton("Right"))
        {
            playerXMomentum = moveSpeed;
        }
        else if (Input.GetButton("Left"))
        {
            playerXMomentum = -moveSpeed;
        }

        // RANGED
        if (Input.GetButtonDown("Fire1"))
        {
            gunCharge = 0f;
        }
        else if (Input.GetButton("Fire1"))
        {
            gunCharge = Mathf.Min(gunCharge + Time.deltaTime * gunChargeSpeed, PlayerRangedScript.maxDamage);
            spriteRenderer.color = Color.Lerp(defaultPlayerColor, chargeColor, gunCharge / PlayerRangedScript.maxDamage);
            playerLight.color = Color.Lerp(defaultLightColor, chargeColor, gunCharge / PlayerRangedScript.maxDamage);
            playerLight.intensity = Mathf.Lerp(defaultLightIntensity, maxLightIntensity, gunCharge / PlayerRangedScript.maxDamage);
            
            if (gunCharge == PlayerRangedScript.maxDamage)
            {
                //playerLight.color = chargeColor;
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            spriteRenderer.color = defaultPlayerColor;
            playerLight.color = defaultLightColor;
            playerLight.intensity = defaultLightIntensity;

            if (gunCharge > minCharge)
            {
                GameObject rangedobj = Instantiate(rangedattack, transform.position, transform.rotation);
                rangedobj.GetComponent<PlayerRangedScript>().damage = gunCharge;
                rangedobj.GetComponentInChildren<Light2D>().intensity = (gunCharge / PlayerRangedScript.maxDamage) * (maxBulletIntensity - minBulletIntensity) + minBulletIntensity;
                if (rangedSfx != null)
                    rangedSfx.Stop();
                rangedSfx = rangedobj.GetComponent<AudioSource>();
            }
            else
            {

            }
        }

        if (Input.GetButton("Fire2")) //MELEE
        {
            if (!meleeCD)
            {
                GameObject meleeobj = Instantiate(meleeattack, transform.position, transform.rotation);
                meleeobj.transform.parent = gameObject.transform;
                meleeobj.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
                meleeobj.transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
                meleeobj.transform.position += transform.up * 0.20f;
                //meleeobj.transform.position += new Vector3(transform.forward.x*2 + 1.0f, transform.forward.y*2 + 1.0f, 0.0f);
                meleeCD = true;
                StartCoroutine("MeleeCooldown");
            }
        }

        if (Input.GetButton("Rotate Left"))
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 10, transform.rotation.z);
            transform.Rotate(0.0f, 0.0f, 0.75f);
        }
        if (Input.GetButton("Rotate Right"))
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 10, transform.rotation.z);
            transform.Rotate(0.0f, 0.0f, -0.75f);
        }

        transform.position += new Vector3(playerXMomentum * Time.deltaTime, playerYMomentum * Time.deltaTime, 0.0f);
        //rb.MovePosition( new Vector2(transform.position.x + playerXMomentum, transform.position.y + playerYMomentum) );

    }

    public void TakeDamage(float damage)
    {
        HEALTH.Damage(damage);
    }


    IEnumerator MeleeCooldown()
    {
        yield return new WaitForSeconds(0.6f);
        meleeCD = false;
    }

}
