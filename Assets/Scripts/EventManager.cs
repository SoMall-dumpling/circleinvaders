public class EventManager
{

    public delegate void OnScoreGained(int amount);
    public event OnScoreGained ScoreGained;

    public void DispatchScoreGained(int value)
    {
        ScoreGained(value);
    }

    public delegate void OnHealthChanged();
    public event OnHealthChanged HealthChanged;

    public void DispatchHealthChanged()
    {
        HealthChanged();
    }

    public delegate void OnLose();
    public event OnLose Lose;

    public void DispatchLose()
    {
        Lose();
    }
	
}
