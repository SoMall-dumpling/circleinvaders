using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject ScoreText;
    public GameObject[] HealthIndicators;
    public GameObject GameEndText;

    private LevelStatsModel levelStatsModel;

	void Start()
    {
        GameEndText.SetActive(false);

        SingletonMapper.Get<EventManager>().ScoreGained += OnScoreGained;
        SingletonMapper.Get<EventManager>().HealthChanged += OnHealthChanged;
        SingletonMapper.Get<EventManager>().HealthChanged += OnLose;

        levelStatsModel = SingletonMapper.Get<LevelStatsModel>();

        UpdateHealth();
        UpdateScore();
    }

    void Update()
    {
        if (levelStatsModel.IsLost() && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnScoreGained(int amount)
    {
        UpdateScore();
    }

    private void OnHealthChanged()
    {
        UpdateHealth();
    }

    private void OnLose()
    {
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

        GameEndText.SetActive(health <= 0);

        for (int i = 0; i < HealthIndicators.Length; i++)
        {
            HealthIndicators[i].SetActive(i < health);
        }
    }
}
