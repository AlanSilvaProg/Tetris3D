using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class EndGameControl : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playingTimeText;
    public GameObject mainWindow;

    private void OnEnable()
    {
        scoreText.text = PlayerStats.playerStats.score.ToString("0000");
        playerNameText.text = PlayerStats.playerStats.playerName;
        playingTimeText.text = "Tempo jogado " + PlayerStats.playerStats.hours.ToString("00") + ":" + PlayerStats.playerStats.minutes.ToString("00") + ":" + PlayerStats.playerStats.seconds.ToString("00");
        RankingControll.SetNewRankOnList(PlayerStats.playerStats);
        mainWindow.transform.DOScale(Vector3.one, 1);
    }

    public void RestartClick()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }

    public void MainMenuClick()
    {
        SceneManager.LoadScene("Menu");
    }
}
