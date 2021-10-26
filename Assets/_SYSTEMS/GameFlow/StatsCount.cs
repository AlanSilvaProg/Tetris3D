using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsCount : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playerName;

    int hours;
    int minutes;
    int seconds;
    [HideInInspector] int playerScore = 0;
    public void IncreaseScore(int score) => playerScore += score;
    public int GetScore() => playerScore;

    float showedScore = 0;

    private void Start()
    {
        playerName.text = PlayerStats.playerStats.playerName;
    }

    private void Update()
    {
        if (showedScore != playerScore) UpdateInformations();
        PlayerStats.playerStats.totalSeconds += Time.deltaTime;
    }

    public void UpdateInformations()
    {
        showedScore += Time.deltaTime * 50;
        showedScore = Mathf.Clamp(showedScore, 0, playerScore);
        scoreText.text = showedScore.ToString("0000");
        PlayerStats.playerStats.score = playerScore;
    }

}
