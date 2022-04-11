using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Depth : Resource
{
    private bool descending;
    
    [Header("Countdown Timer")]
    public TMP_Text timer;
    private string timerFormat = "00";

    [Header("Lighting")]
    public Light2D globalLight;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;

    [Header("Enemy Spawning")]
    public GameObject Powersource;
    PowersourceScript PSS;
    int waveNumber = 0;
    public float waveInterval = 15f; //seconds
    public float spawnIntervalMin = 3f;
    public float spawnIntervalMax = 5f;
    float prevSpawnTime;
    float spawnInterval;
    public float bruteSpawnProbability = 0.2f;

    [Header("Win Condition")]
    public GameObject winScreen;
    bool winCondition = false;
    public GameObject enemiesWarning;


    override protected void Start()
    {
        wholeNumbers = false;
        startValue = 0f;
        base.Start();
        descending = true;

        PSS = Powersource.GetComponent<PowersourceScript>();
        prevSpawnTime = 0f;
        Random.InitState(System.DateTime.Now.Millisecond);
        spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);

        winScreen.SetActive(false);
        enemiesWarning.SetActive(false);
    }

    void Update()
    {
        if (descending)
        {
            // update depth and timer
            SetValue(currentValue + Time.deltaTime);

            if (currentValue == maxValue)
            {
                descending = false;
            }

            float timeRemaining = maxValue - currentValue;

            int totalSeconds = Mathf.CeilToInt(timeRemaining);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            timer.text = minutes.ToString(timerFormat) + ":" + seconds.ToString(timerFormat);

            float intensityScale = timeRemaining / maxValue;
            globalLight.intensity = Mathf.Pow(intensityScale, 1.5f) * (maxIntensity - minIntensity) + minIntensity;


            // spawn enemies
            if (currentValue / waveInterval > waveNumber) // wave
            {
                PSS.SpawnBrute((int)( (float)waveNumber * 1f/5f + 0f ));
                PSS.SpawnRat((int)( Mathf.Ceil((float)waveNumber * 1f/4f) + 2f ));

                waveNumber++;
            }
            if ((currentValue - prevSpawnTime) > spawnInterval) // individual randomized spawns
            {
                if (Random.Range(0f,1f) < bruteSpawnProbability)
                {
                    PSS.SpawnBrute(1);
                }
                else
                {
                    if (Random.Range(0f,1f) < 0.5f)
                        PSS.SpawnRat(1);
                    else
                        PSS.SpawnRat(2);
                }

                prevSpawnTime = currentValue;
                spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            }
        }
        else if (!winCondition && currentValue == maxValue && PowersourceScript.enemyCount == 0)
        {
            winCondition = true;
            winScreen.SetActive(true);
            StartCoroutine("Victory");
        }
        else if (currentValue == maxValue && PowersourceScript.enemyCount != 0)
        {
            timer.gameObject.SetActive(false);
            enemiesWarning.SetActive(true);
        }

    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(3.5f);
        GameManager.Instance.MainMenu();
    }
}
