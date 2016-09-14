using UnityEngine;
using System.Collections;

public class DestroyOnBullet : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        { 
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
