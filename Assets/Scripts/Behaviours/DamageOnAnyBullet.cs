using UnityEngine;
using System.Collections;

public class DamageOnAnyBullet : MonoBehaviour
{



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
