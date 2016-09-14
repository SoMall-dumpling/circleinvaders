using UnityEngine;
using System.Collections;

public class LoseOnTouchPlanet : MonoBehaviour {  
	
	void Update ()
    {
        if (transform.position.magnitude < LevelConstants.PLANET_RADIUS)
        {
            SingletonMapper.Get<LevelStatsModel>().CurrentHealth = 0;
        }
    }
}
