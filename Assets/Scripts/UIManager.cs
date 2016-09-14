using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject ScoreText;
    public GameObject[] HealthIndicators;

    private LevelStatsModel levelStatsModel;

	void Start()
    {
        SingletonMapper.Get<EventManager>().ScoreGained += OnScoreGained;
        SingletonMapper.Get<EventManager>().HealthChanged += OnHealthChanged;

        levelStatsModel = SingletonMapper.Get<LevelStatsModel>();

        UpdateHealth();
        UpdateScore();
    }

    private void OnScoreGained(int amount)
    {
        UpdateScore();
    }

    private void OnHealthChanged()
    {
        UpdateHealth();
    }

    private void UpdateScore()
    {
        if (ScoreText) { 
            ScoreText.GetComponent<Text>().text = levelStatsModel.CurrentScore.ToString();
        }
    }

    private void UpdateHealth()
    {
        int health = levelStatsModel.CurrentHealth;
        for (int i = 0; i < HealthIndicators.Length; i++)
        {
            HealthIndicators[i].SetActive(i < health);
        }
    }
}
