using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float DRILL_SPEED;
    
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, DRILL_SPEED * Time.deltaTime);
    }
}
