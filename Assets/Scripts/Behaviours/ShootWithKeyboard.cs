using UnityEngine;
using System.Collections;

public class ShootWithKeyboard : MonoBehaviour
{

    public GameObject Bullet;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
        ShootPlayerHandler.Shoot(bullet, transform.up);
    }
}
