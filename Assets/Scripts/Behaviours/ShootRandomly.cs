using UnityEngine;
using System.Collections;

public class ShootRandomly : MonoBehaviour
{

    public GameObject Bullet;

    private EnemyProperties enemyProperties;

    void Start()
    {
        enemyProperties = GetComponent<EnemyProperties>();
        InvokeRepeating("Shoot", 1, 2);
    }

    void Shoot()
    {
        if (SingletonMapper.Get<LevelStatsModel>().IsLost())
        {
            CancelInvoke();
            return;
        }

        if (Random.Range(0, 100) < enemyProperties.ShootingRate)
        { 
            GameObject bullet = Instantiate(Bullet, transform.position - transform.up.normalized, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce(-transform.up * 2, ForceMode2D.Impulse);
        }
    }
}
