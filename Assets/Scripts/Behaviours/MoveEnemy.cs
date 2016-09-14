using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {

    public float StepTime = 1;

    private int stepsOnCircleNum;

    private int stepsOnCircleDirection = 1;
    private int stepsOnCircleCounter = 0;

    // TODO get rid of magic numbers

    void Start()
    {
        stepsOnCircleNum = 10;
        InvokeRepeating("Move", StepTime, StepTime);
    }

    void Move()
    {
        if (stepsOnCircleCounter < stepsOnCircleNum)
        {
            MoveOnCircle();
            stepsOnCircleCounter++;
        }
        else
        {
            MoveTowardsPlanet();
            stepsOnCircleCounter = 0;
            stepsOnCircleDirection *= -1;
        }
    }

    void MoveOnCircle()
    {
        float distance = transform.position.magnitude;
        float currentAngleRadian = Mathf.Atan2(transform.position.y, transform.position.x);
        float newAngleRadian = currentAngleRadian + Mathf.PI / 30 * stepsOnCircleDirection;

        Vector3 newPosition = new Vector3(distance * Mathf.Cos(newAngleRadian), distance * Mathf.Sin(newAngleRadian), 0);
        transform.position = newPosition;

        float angleInDegrees = newAngleRadian * (180 / Mathf.PI) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angleInDegrees);
    }

    void MoveTowardsPlanet()
    {
        if (SingletonMapper.Get<LevelStatsModel>().IsLost())
        {
            return;
        }

        Vector3 newPosition = transform.position;
        Vector3 moveDirection = -transform.position.normalized;
        newPosition = transform.position + moveDirection * EnemyConstants.ENEMY_ROW_DISTANCE;
        transform.position = newPosition;
    }
}
