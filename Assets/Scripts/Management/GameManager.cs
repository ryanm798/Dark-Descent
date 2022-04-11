using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /***** SINGLETON SETUP *****/
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        if (this == _instance)
        {
            _instance = null;
        }
    }
    /******************************/

    private Animator transitionAnimator;
    private int sceneToLoad = 0;

    private void Start()
    {
        ScaleTime(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameObject transitionObj = GameObject.Find("Transition Animator");
        if (transitionObj != null)
            transitionAnimator = transitionObj.GetComponent<Animator>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ScaleTime(1);
        GameObject transitionObj = GameObject.Find("Transition Animator");
        if (transitionObj != null)
            transitionAnimator = transitionObj.GetComponent<Animator>();
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        sceneToLoad = 1;
        LoadScene();
    }

    public void MainMenu()
    {
        sceneToLoad = 0;
        LoadScene();
    }

    public void Restart()
    {
        sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        LoadScene();
    }

    public void LoadScene()
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("TransitionOut");
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void LoadSceneNoTransition()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ScaleTime(float tscale)
    {
        Time.timeScale = tscale;
        Time.fixedDeltaTime = tscale * 0.02f;
    }
}
