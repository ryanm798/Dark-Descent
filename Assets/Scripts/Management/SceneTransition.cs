using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public void OnTransitionInEnd()
    {

    }

    public void OnTransitionOutEnd()
    {
        GameManager.Instance.LoadSceneNoTransition();
    }
}
