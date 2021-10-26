using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PuzzleConfigure puzzleAcess;
    public ObjectPooling poolAcess;
    public StatsCount statsAcess;
    public GameObject mainHUD;
    public GameObject gameOverHUD;

    private void Start()
    {
        if (PlayerStats.playerStats == null) PlayerStats.InitializeNewPlayer("No Name");
        InitializeGameManager();
    }

    public void InitializeGameManager()
    {
        if (GameManager.Instance != null) Destroy(gameObject); else Instance = this;
        puzzleAcess.enabled = true;
    }

    public void EndGame()
    {
        PlayerStats.playerStats.score = statsAcess.GetScore();
        PlayerStats.playerStats.hours = TimeSpan.FromSeconds(PlayerStats.playerStats.totalSeconds).Hours;
        PlayerStats.playerStats.minutes = TimeSpan.FromSeconds(PlayerStats.playerStats.totalSeconds).Minutes;
        PlayerStats.playerStats.seconds = TimeSpan.FromSeconds(PlayerStats.playerStats.totalSeconds).Seconds;
        mainHUD.SetActive(false);
        gameOverHUD.SetActive(true);
    }
}
