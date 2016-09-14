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

}
