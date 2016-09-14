using UnityEngine;
using System.Collections;

public class ShootRandomly : MonoBehaviour
{

    public GameObject Bullet;

    void Start()
    {
        InvokeRepeating("Shoot", 1, 2);
    }

    void Shoot()
    {
        if (SingletonMapper.Get<LevelStatsModel>().IsLost())
        {
            CancelInvoke();
            return;
        }

        if (Random.Range(0, 100) < 2)
        { 
            GameObject bullet = Instantiate(Bullet, transform.position - transform.up.normalized, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce(-transform.up * 2, ForceMode2D.Impulse);
        }
    }
}
