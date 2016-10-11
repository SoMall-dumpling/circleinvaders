using UnityEngine;

public class DestroyOnTouchPlanet : MonoBehaviour
{

    void Update()
    {
        if (transform.position.magnitude < LevelConstants.PLANET_RADIUS)
        {
            SingletonMapper.Get<EventManager>().DispatchEnemyDestroyed(gameObject);
            Destroy(gameObject);
        }
    }
}
