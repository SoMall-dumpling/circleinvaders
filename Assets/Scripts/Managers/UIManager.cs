using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject ScoreText;
    public GameObject WaveText;
    public GameObject[] HealthIndicators;
    public GameObject GameEndText;

    private LevelStatsModel levelStatsModel;

	void Start()
    {
        GameEndText.SetActive(false);

        SingletonMapper.Get<EventManager>().ScoreGained += OnScoreGained;
        SingletonMapper.Get<EventManager>().HealthChanged += OnHealthChanged;
        SingletonMapper.Get<EventManager>().WaveStarted += OnWaveStarted;
        SingletonMapper.Get<EventManager>().Lose += OnLose;

        levelStatsModel = SingletonMapper.Get<LevelStatsModel>();

        UpdateHealth();
        UpdateScore();
        UpdateWave();
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
        UpdateComponentVisibility();
    }

    private void OnWaveStarted()
    {
        UpdateComponentVisibility();
        UpdateWave();
    }

    private void UpdateScore()
    {
        if (ScoreText)
        { 
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

    void UpdateWave()
    {
        WaveText.GetComponent<Text>().text = "Wave " + levelStatsModel.CurrentWaveNumber.ToString();
    }

    void UpdateComponentVisibility()
    {
        GameEndText.SetActive(levelStatsModel.IsLost());
        ScoreText.SetActive(!levelStatsModel.IsLost());
        WaveText.SetActive(!levelStatsModel.IsLost());
    }
}
