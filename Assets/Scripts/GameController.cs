using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public GameConfig gameConfig;
    private float initialGameTimer;

    private IEnumerator gameTimeCoroutine; 

    void Start()
    {
       initialGameTimer = gameConfig.configData.GameTimer;
        gameTimeCoroutine = GameTimer();

        GameEvents.GameStart.AddListener(StartGame);
        GameEvents.GameOver.AddListener(EndGame);

        GameEvents.GameStart.Invoke();
    }

    private void EndGame()
    {
        StopCoroutine(gameTimeCoroutine);
        resetGameTimerToOrignal();
    }

    private void StartGame()
    {
        StartCoroutine(gameTimeCoroutine);
    }

    private void resetGameTimerToOrignal()
    {
        gameConfig.configData.GameTimer = initialGameTimer;
        gameConfig.Notify();
    }
    IEnumerator GameTimer()
    {
        while(gameConfig.configData.GameTimer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            gameConfig.configData.GameTimer--;
            gameConfig.Notify();
        }
    }

    private void OnDestroy()
    {
        resetGameTimerToOrignal();
    }
}
