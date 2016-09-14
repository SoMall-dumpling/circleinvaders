using UnityEngine;

public class InitializeLevel : MonoBehaviour {
    
	void Awake()
    {
        SingletonMapper.Map<EventManager>(new EventManager());
        SingletonMapper.Map<LevelStatsModel>(new LevelStatsModel());
    }
}
