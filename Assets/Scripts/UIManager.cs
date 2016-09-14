using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject ScoreText;

	void Start()
    {
        SingletonMapper.Get<EventManager>().ScoreGained += OnScoreGained;
	}

    private void OnScoreGained(int amount)
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        ScoreText.GetComponent<Text>().text = SingletonMapper.Get<LevelStatsModel>().CurrentScore.ToString();
    }
}
