using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingSlot : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public void InitializeSlot(PlayerStatsInfo stats)
    {
        nameText.text = stats.playerName;
        scoreText.text = stats.score.ToString("0000");
        timeText.text = stats.hours.ToString("00") + ":" + stats.minutes.ToString("00") + ":" + stats.seconds.ToString("00");
    }
}
