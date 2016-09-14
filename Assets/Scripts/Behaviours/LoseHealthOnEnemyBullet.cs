using UnityEngine;
using System.Collections;

public class LoseHealthOnEnemyBullet : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            SingletonMapper.Get<LevelStatsModel>().CurrentHealth -= 1;
        }
    }
}
