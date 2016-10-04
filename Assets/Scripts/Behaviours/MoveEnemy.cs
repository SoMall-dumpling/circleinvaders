using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    public float StepTime = 0.9f;

    private int stepsOnCircleNum = 0;
    private int stepsOnCircleDirection = 1;
    private int stepsOnCircleCounter = 0;

    private EnemyProperties enemyProperties;
    private EnemyMovementSettingsVO movementSettings;

    void Start()
    {
        enemyProperties = GetComponent<EnemyProperties>();
    }

    public void SetSettings(EnemyMovementSettingsVO value)
    {
        movementSettings = value;
        stepsOnCircleNum = Mathf.FloorToInt(movementSettings.MaxAngle / EnemyConstants.ENEMY_STEP_ANGLE);
        stepsOnCircleDirection = movementSettings.StartDirection;
        CancelInvoke();
        InvokeRepeating("Move", StepTime, StepTime);
    }

    void Move()
    {
        if (movementSettings == null) return;

        switch (movementSettings.MovementType)
        {
            case EnemyMovementTypeEnum.Spiral:
                float stepsInFullCircle = Mathf.PI * 2 / (EnemyConstants.ENEMY_STEP_ANGLE * enemyProperties.MovementSpeed);
                MoveOnCircle(EnemyConstants.ENEMY_ROW_DISTANCE / stepsInFullCircle * 1.5f);
                break;

            default:
            case EnemyMovementTypeEnum.ZigZag:
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
                break;
        }
    }

    void MoveOnCircle(float moveDownDistance = 0)
    {
        float distance = transform.position.magnitude;
        float currentAngleRadian = Mathf.Atan2(transform.position.y, transform.position.x);
        float newAngleRadian = currentAngleRadian + EnemyConstants.ENEMY_STEP_ANGLE * enemyProperties.MovementSpeed * stepsOnCircleDirection;

        Vector3 newPosition = new Vector3(distance * Mathf.Cos(newAngleRadian), distance * Mathf.Sin(newAngleRadian), 0);
        newPosition = newPosition + (-newPosition.normalized * moveDownDistance);
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

        if (enemyProperties.IsApproaching)
        {
            Vector3 newPosition = transform.position;
            Vector3 moveDirection = -transform.position.normalized;
            newPosition = transform.position + moveDirection * EnemyConstants.ENEMY_ROW_DISTANCE * 0.5f;
            transform.position = newPosition;
        }
    }
}
