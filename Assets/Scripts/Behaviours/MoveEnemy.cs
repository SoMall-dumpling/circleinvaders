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
        if (movementSettings.IsStepWise)
        {
            CancelInvoke();
            InvokeRepeating("Move", StepTime, StepTime);
        }
    }

    void Update()
    {
        if (!movementSettings.IsStepWise)
        {
            Move(Time.deltaTime);
        }
    }

    void Move()
    {
        Move(1);
    }

    void Move(float timeFactor = 1)
    {
        if (movementSettings == null) return;

        switch (movementSettings.MovementType)
        {
            case EnemyMovementTypeEnum.Spiral:
                float stepsInFullCircle = Mathf.PI * 2 / (EnemyConstants.ENEMY_STEP_ANGLE * enemyProperties.MovementSpeed);
                MoveOnCircle(timeFactor, EnemyConstants.ENEMY_ROW_DISTANCE / stepsInFullCircle * 1.5f);
                break;

            case EnemyMovementTypeEnum.Circle:
                MoveOnCircle(timeFactor);
                break;

            default:
            case EnemyMovementTypeEnum.ZigZag:
                if (stepsOnCircleCounter < stepsOnCircleNum)
                {
                    MoveOnCircle(timeFactor);
                    stepsOnCircleCounter++;
                }
                else
                {
                    MoveTowardsPlanet(timeFactor);
                    stepsOnCircleCounter = 0;
                    stepsOnCircleDirection *= -1;
                }
                break;
        }
    }

    void MoveOnCircle(float timeFactor = 1, float moveDownDistance = 0)
    {
        float distance = transform.position.magnitude;
        float currentAngleRadian = Mathf.Atan2(transform.position.y, transform.position.x);
        float newAngleRadian = currentAngleRadian + EnemyConstants.ENEMY_STEP_ANGLE * enemyProperties.MovementSpeed * stepsOnCircleDirection * timeFactor;

        Vector3 newPosition = new Vector3(distance * Mathf.Cos(newAngleRadian), distance * Mathf.Sin(newAngleRadian), 0);
        newPosition = newPosition + (-newPosition.normalized * moveDownDistance);
        transform.position = newPosition;

        float angleInDegrees = newAngleRadian * (180 / Mathf.PI) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angleInDegrees);
    }

    void MoveTowardsPlanet(float timeFactor = 1)
    {
        if (SingletonMapper.Get<LevelStatsModel>().IsLost())
        {
            return;
        }

        if (enemyProperties.IsApproaching)
        {
            Vector3 newPosition = transform.position;
            Vector3 moveDirection = -transform.position.normalized;
            newPosition = transform.position + moveDirection * EnemyConstants.ENEMY_ROW_DISTANCE * 0.5f * timeFactor;
            transform.position = newPosition;
        }
    }
}
