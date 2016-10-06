using UnityEngine;
using System.Collections;

public class ShootWithTouch : MonoBehaviour
{

    // TODO pool bullets
    // TODO get prefab dynamically instead of passing the same to two different Behaviours

    public GameObject Bullet;

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Shoot();
                    return;
            }
        }

        // for testing with mouse
        if (Input.GetMouseButtonDown(1))
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
