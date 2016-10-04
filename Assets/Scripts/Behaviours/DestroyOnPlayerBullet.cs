using UnityEngine;
using System.Collections;

public class DestroyOnPlayerBullet : MonoBehaviour
{

    EnemyProperties enemyProperties;

    void Start()
    {
        enemyProperties = GetComponent<EnemyProperties>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            enemyProperties.CurrentHits++;
            if (enemyProperties.CurrentHits == enemyProperties.HitsToKill)
            {
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
