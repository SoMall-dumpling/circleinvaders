using UnityEngine;

public class ShootPlayerHandler
{

    // TODO make this a non-static event listener
    public static void Shoot(GameObject bullet, Vector3 direction)
    {
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * 10, ForceMode2D.Impulse);
    }

}
