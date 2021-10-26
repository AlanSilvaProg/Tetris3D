using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingScreen : MonoBehaviour
{
    public RankingSlot[] rankingSlots;
    List<PlayerStatsInfo> rankingList;
    bool hasInitialized = false;

    private void OnEnable()
    {
        if (hasInitialized) return;
        rankingList = RankingControll.GetRank();
        for(int i = 0; i < rankingList.Count; i++)
        {
            if (rankingSlots[i] != null)
                rankingSlots[i].InitializeSlot(rankingList[i]);
            else
                break;
        }
        hasInitialized = true;
    }

}
