public class LevelStatsModel
{

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
        }
    }

}
