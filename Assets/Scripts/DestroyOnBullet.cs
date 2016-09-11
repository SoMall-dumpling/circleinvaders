using UnityEngine;
using System.Collections;

public class DestroyOnBullet : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
