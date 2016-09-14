public class EventManager
{

    public delegate void OnScoreGained(int amount);
    public event OnScoreGained ScoreGained;

    public void DispatchScoreGained(int value)
    {
        ScoreGained(value);
    }

	
}
