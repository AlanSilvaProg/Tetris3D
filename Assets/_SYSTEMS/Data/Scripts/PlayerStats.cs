using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

[System.Serializable]
public class PlayerStatsInfo
{
    public int score;
    public string playerName;
    public float totalSeconds;
    public float hours;
    public float minutes;
    public float seconds;

    public PlayerStatsInfo()
    {
        score = 0;
        playerName = "";
        totalSeconds = 0;
        hours = 0;
        minutes = 0;
        seconds = 0;
    }
    public PlayerStatsInfo(string playerName)
    {
        score = 0;
        this.playerName = playerName;
        totalSeconds = 0;
        hours = 0;
        minutes = 0;
        seconds = 0;
    }

    public PlayerStatsInfo(PlayerStatsInfo stats)
    {
        score = stats.score;
        playerName = stats.playerName;
        totalSeconds = stats.totalSeconds;
        hours = stats.hours;
        minutes = stats.minutes;
        seconds = stats.seconds;
    }
}

public static class PlayerStats
{
    public static PlayerStatsInfo playerStats;
    static bool loaded = false;
    
    public static void InitializeNewPlayer(string playerName)
    {
        playerStats = new PlayerStatsInfo(playerName);
    }

    public static void SaveStats()
    {
        SaveSystem<PlayerStatsInfo>.SaveGame(playerStats, "PlayerStats:" + playerStats.playerName);
    }

    public static void LoadStats()
    {
        if (!loaded)
        {
            loaded = true;
            if (SaveSystem<PlayerStatsInfo>.HasDataToLoad("PlayerStats:" + playerStats.playerName))
                playerStats = new PlayerStatsInfo(SaveSystem<PlayerStatsInfo>.LoadGameInfo("PlayerStats:" + playerStats.playerName));
            else
                playerStats = new PlayerStatsInfo();
        }
    }
}
