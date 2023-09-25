using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState { Menu, Game, LevelComplete, GameOver }

    private GameState gameState;

    public static Action<GameState> onGameStateChanged;
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
        print("click");
    }
    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
