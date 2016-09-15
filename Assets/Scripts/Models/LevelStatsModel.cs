public class LevelStatsModel
{

    private int _currentWaveNumber = 0;
    public int CurrentWaveNumber
    {
        get { return _currentWaveNumber;  }
        set
        {
            _currentWaveNumber = value;
            SingletonMapper.Get<EventManager>().DispatchWaveStarted();
        }
    }

    private int _currentScore = 0;
    public int CurrentScore {
        get { return _currentScore; }
        set
        {
            _currentScore = value;
            SingletonMapper.Get<EventManager>().DispatchScoreGained(value);
        }
    }

    private int _currentHealth = LevelConstants.MAX_HEALTH;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            SingletonMapper.Get<EventManager>().DispatchHealthChanged();
            if (_currentHealth <= 0)
            {
                SingletonMapper.Get<EventManager>().DispatchLose();
            }
        }
    }

    public bool IsLost()
    {
        return _currentHealth <= 0;
    }

}
