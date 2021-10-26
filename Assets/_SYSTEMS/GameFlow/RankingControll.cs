using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
using System.Linq;

public static class RankingControll
{
    static List<PlayerStatsInfo> rankingList = new List<PlayerStatsInfo>();
    static DataToSave data = new DataToSave();
    const string RANKING_SAVE_DATA = "Ranking";

    static void SaveRanking()
    {
        data.rankList = rankingList;
        SaveSystem<DataToSave>.SaveGame(data, RANKING_SAVE_DATA);
    }

    public static void LoadRank()
    {
        if (SaveSystem<DataToSave>.HasDataToLoad(RANKING_SAVE_DATA)) rankingList = SaveSystem<DataToSave>.LoadGameInfo(RANKING_SAVE_DATA).rankList;
    }

    public static List<PlayerStatsInfo> GetRank()
    {
        LoadRank();
        rankingList = (from c in rankingList
                       orderby c.score descending
                       select c).ToList();
        return rankingList;
    }

    public static void SetNewRankOnList(PlayerStatsInfo newRank)
    {
        LoadRank();
        rankingList.Add(newRank);
        SaveRanking();
    }

}
