using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameoverSound;

    private void Start()
    {
        PlayerDetection.onDoorHit += PlayDoorHitSound;

        GameManager.onGameStateChanged += GameStateChangedCallBack;

        Enemy.onRunnerDied += PlayerRunnerDiedSound;
    }
    private void OnDestroy()
    {
        PlayerDetection.onDoorHit -= PlayDoorHitSound;

        GameManager.onGameStateChanged -= GameStateChangedCallBack;

        Enemy.onRunnerDied -= PlayerRunnerDiedSound;
    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.LevelComplete)
            levelCompleteSound.Play();
        else if(gameState == GameManager.GameState.GameOver)
            gameoverSound.Play();
    }
    private void PlayDoorHitSound()
    {
        doorHitSound.Play();
    }

    private void PlayerRunnerDiedSound()
    {
        runnerDieSound.Play();  
    }
    public void DisableSounds()
    {
        doorHitSound.volume = 0;
        runnerDieSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameoverSound.volume = 0;
        buttonSound.volume = 0;
    }

    public void EnableSounds()
    {
        buttonSound.volume = 1;
        doorHitSound.volume = 1;
        runnerDieSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameoverSound.volume = 1;
    }
}
