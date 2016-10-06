using UnityEngine;

public class MovePlayerHandler
{

    // TODO make non-static event listener
    public static void MoveAngle(GameObject player, float radians)
    {
        float r = player.transform.position.magnitude;

        float currentAngleRadian = Mathf.Atan2(player.transform.position.y, player.transform.position.x);
        float newAngleRadian = currentAngleRadian + radians * Time.deltaTime;

        float currentAngleDegree = currentAngleRadian * (180 / Mathf.PI);
        float newAngleDegree = newAngleRadian * (180 / Mathf.PI);

        Vector2 newPosition = player.transform.position;
        newPosition.x = r * Mathf.Cos(newAngleRadian);
        newPosition.y = r * Mathf.Sin(newAngleRadian);
        player.transform.position = newPosition;

        player.transform.Rotate(new Vector3(0, 0, 1) * radians * 180 / Mathf.PI * Time.deltaTime);
    }
}
