using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject Player;
    
    void LateUpdate()
    {
        gameObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, gameObject.transform.position.z);

    }
}
