using UnityEngine;

public class DestroyOnTouchPlanet : MonoBehaviour
{

    void Update()
    {
        if (transform.position.magnitude < LevelConstants.PLANET_RADIUS)
        {
            if (gameObject.tag == TagConstants.ENEMY)
            {
                SingletonMapper.Get<EventManager>().DispatchEnemyDestroyed(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
