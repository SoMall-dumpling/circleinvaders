using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScout : MonoBehaviour
{

    public GameObject Enemy;

    private LevelStatsModel levelStatsModel;
    private bool hasScout = false;

    void Start()
    {
        levelStatsModel = SingletonMapper.Get<LevelStatsModel>();
        SingletonMapper.Get<EventManager>().EnemyDestroyed += OnEnemyDestroyed;
    }

    void Update()
    {
        if (!levelStatsModel.IsWavePending)
        {
            if (!hasScout && Random.Range(0.0f, 1.0f) > 0.98)
            {
                hasScout = true;
                SpawnScout();
            }
        }
    }

    void OnEnemyDestroyed(GameObject enemy)
    {
        EnemyProperties enemyProperties = enemy.GetComponent<EnemyProperties>();
        if (enemyProperties.EnemyType == EnemyTypeEnum.Scout)
        {
            hasScout = false;
        }
    }

    void SpawnScout()
    {
        EnemyMovementSettingsVO movementSettings = new EnemyMovementSettingsVO(EnemyFormationEnum.Circle, EnemyMovementTypeEnum.Circle, 0, 1, false);
        float angle = Random.Range(0, Mathf.PI * 2);
        SpawnEnemyAt(LevelConstants.MIN_ENEMY_SPAWN_DISTANCE + 5 * EnemyConstants.ENEMY_ROW_DISTANCE, angle, EnemyTypeEnum.Scout, movementSettings);
    }

    void SpawnEnemyAt(float distance, float angle, EnemyTypeEnum enemyType, EnemyMovementSettingsVO movementSettings)
    {
        // create enemy game object
        Vector3 position = new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);
        float angleInDegrees = angle * (180 / Mathf.PI) - 90;
        Quaternion rotation = Quaternion.Euler(0, 0, angleInDegrees);
        GameObject enemy = Instantiate(Enemy, position, rotation, transform) as GameObject;

        // set enemy type
        // TODO create prefabs / templates to instantiate instead of setting these for every instance
        CircleCollider2D collider = enemy.GetComponent<CircleCollider2D>();
        EnemyProperties enemyProperties = enemy.GetComponent<EnemyProperties>();
        enemyProperties.EnemyType = enemyType;
        enemyProperties.HitsToKill = enemyType == EnemyTypeEnum.Big ? 2 : 1;
        enemyProperties.MovementSpeed = enemyType == EnemyTypeEnum.Scout ? 2 : 1;
        enemyProperties.IsApproaching = enemyType == EnemyTypeEnum.Scout ? false : true;
        switch (enemyType)
        {
            case EnemyTypeEnum.Basic:
                enemyProperties.ShootingRate = 0;
                collider.radius = 0.25f;
                break;
            case EnemyTypeEnum.Big:
                enemyProperties.ShootingRate = 1;
                collider.radius = 0.28f;
                break;
            case EnemyTypeEnum.Small:
                enemyProperties.ShootingRate = 1;
                collider.radius = 0.12f;
                break;
            case EnemyTypeEnum.Shooter:
                enemyProperties.ShootingRate = 5;
                collider.radius = 0.22f;
                break;
            case EnemyTypeEnum.Scout:
                enemyProperties.ShootingRate = 10;
                collider.radius = 0.22f;
                break;
        }

        // set movement settings
        MoveEnemy moveComponent = enemy.GetComponent<MoveEnemy>();
        moveComponent.SetSettings(movementSettings);
    }
}
