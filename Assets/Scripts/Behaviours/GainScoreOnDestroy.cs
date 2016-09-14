using UnityEngine;
using System.Collections;

public class GainScoreOnDestroy : MonoBehaviour {

    public int ScoreValue;

    void OnDestroy()
    {
        SingletonMapper.Get<LevelStatsModel>().CurrentScore += ScoreValue;
    }
}
