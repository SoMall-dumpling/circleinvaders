using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject Enemy;

    private EnemyFormationEnum[] formations = new EnemyFormationEnum[]
    {
        EnemyFormationEnum.FourBalanced,
        EnemyFormationEnum.TwoSmall,
        EnemyFormationEnum.Spiral,
        EnemyFormationEnum.VerticalLines,
        EnemyFormationEnum.FourUnbalanced,
        EnemyFormationEnum.TwoBig,
        EnemyFormationEnum.HorizontalLines,
        EnemyFormationEnum.Zigzag
    };

    private LevelStatsModel levelStatsModel;
    private bool wavePending = false;
    private bool hasScout = false;

    void Start()
    {
        levelStatsModel = SingletonMapper.Get<LevelStatsModel>();
        StartWave();
    }

    void Update()
    {
        if (!wavePending)
        {
            int enemyCount = GameObject.FindGameObjectsWithTag(TagConstants.ENEMY).Length;
            if (enemyCount == 0)
            {
                wavePending = true;
                Invoke("StartWave", 2);
            }

            if (!hasScout && Random.Range(0.0f, 1.0f) > 0.98)
            {
                hasScout = true;
                SpawnScout();
            }
        }
    }

    void StartWave()
    {
        levelStatsModel.CurrentWaveNumber++;
        EnemyFormationEnum currentFormation = formations[(levelStatsModel.CurrentWaveNumber - 1) % formations.Length];
        SpawnEnemies(currentFormation);
    }

    void SpawnEnemies(EnemyFormationEnum formation)
    {
        if (Debug.isDebugBuild) Debug.Log("Spawning enemies in formation " + formation);
        int maxEnemiesOnCircle = EnemyConstants.MAX_ENEMIES_ON_CIRCLE;

        EnemyMovementSettingsVO movementSettings;
        int enemiesPerBlock;
        switch (formation)
        {
            case EnemyFormationEnum.FourBalanced:
                enemiesPerBlock = Mathf.FloorToInt(maxEnemiesOnCircle / 4);
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.ZigZag, Mathf.PI / 2, 1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, 0, enemiesPerBlock - 1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, enemiesPerBlock, enemiesPerBlock * 2 - 1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, enemiesPerBlock * 2, enemiesPerBlock * 3 - 1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, enemiesPerBlock * 3, enemiesPerBlock * 4 - 1);
                break;

            case EnemyFormationEnum.FourUnbalanced:
                enemiesPerBlock = Mathf.FloorToInt(maxEnemiesOnCircle / 4);
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.ZigZag, Mathf.PI / 2, 1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, 0, enemiesPerBlock - 1, 0, 4);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Shooter, enemiesPerBlock, enemiesPerBlock * 2 - 1, 2, 4);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, enemiesPerBlock * 2, enemiesPerBlock * 3 - 1, 0, 4);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Shooter, enemiesPerBlock * 3, enemiesPerBlock * 4 - 1, 2, 4);
                break;

            case EnemyFormationEnum.HorizontalLines:
                enemiesPerBlock = 7;
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.ZigZag, Mathf.PI * 2 / 3, -1);
                for (int i = 0; i < EnemyConstants.MAX_ENEMY_ROWS; i++)
                {
                    movementSettings.StartDirection = i % 2 == 0 ? -1 : 1;
                    SpawnEnemyBlock(movementSettings,
                        i == 0 ? EnemyTypeEnum.Small : i == 1 ? EnemyTypeEnum.Big : i == 3 ? EnemyTypeEnum.Shooter : EnemyTypeEnum.Basic,
                        i * enemiesPerBlock + i, i * enemiesPerBlock + enemiesPerBlock + i, i, i + 1);
                }
                break;

            case EnemyFormationEnum.Spiral:
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.Spiral, Mathf.PI * 2, -1);
                float circlePosRadians = Mathf.PI * 2 / EnemyConstants.MAX_ENEMIES_ON_CIRCLE;
                float distance = LevelConstants.MIN_ENEMY_SPAWN_DISTANCE;
                float angle = 0;
                while (distance < LevelConstants.MIN_ENEMY_SPAWN_DISTANCE + EnemyConstants.MAX_ENEMY_ROWS * EnemyConstants.ENEMY_ROW_DISTANCE)
                {
                    SpawnEnemyAt(distance, angle, EnemyTypeEnum.Basic, movementSettings);
                    distance += (EnemyConstants.ENEMY_ROW_DISTANCE / EnemyConstants.MAX_ENEMIES_ON_CIRCLE) * 1.5f;
                    angle += circlePosRadians;
                }
                break;

            case EnemyFormationEnum.TwoBig:
                enemiesPerBlock = Mathf.FloorToInt(maxEnemiesOnCircle / 2);
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.ZigZag, Mathf.PI, 1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Big, 0, enemiesPerBlock - 1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Small, enemiesPerBlock, enemiesPerBlock * 2 - 1);
                break;

            case EnemyFormationEnum.TwoSmall:
                enemiesPerBlock = Mathf.FloorToInt(maxEnemiesOnCircle / 4);
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.ZigZag, Mathf.PI * 2 / 8, 1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, 0, enemiesPerBlock - 1);
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.ZigZag, Mathf.PI * 2 / 8, -1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Big, enemiesPerBlock * 2, enemiesPerBlock * 3 - 1);
                break;

            case EnemyFormationEnum.VerticalLines:
                int numVerticalLines = 8;
                int blockDistance = EnemyConstants.MAX_ENEMIES_ON_CIRCLE / numVerticalLines;
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.ZigZag, Mathf.PI * 2 / 8, 1);
                for (int i = 0; i < numVerticalLines; i++)
                {
                    SpawnEnemyBlock(movementSettings, i % 2 == 0 ? EnemyTypeEnum.Basic : EnemyTypeEnum.Big, i * blockDistance, i * blockDistance + 1);
                }
                break;

            case EnemyFormationEnum.Zigzag:
                enemiesPerBlock = EnemyConstants.MAX_ENEMIES_ON_CIRCLE / 8;
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.ZigZag, Mathf.PI * 2 / 8, 1);
                for (int i = 0; i < EnemyConstants.MAX_ENEMIES_ON_CIRCLE; i++)
                {
                    SpawnEnemyBlock(movementSettings, i % 2 == 0 ? EnemyTypeEnum.Big : EnemyTypeEnum.Small, i * enemiesPerBlock, i * enemiesPerBlock + enemiesPerBlock, i % 2 == 0 ? 0 : 2, i % 2 == 0 ? 2 : 4);
                }
                break;

            default:
                enemiesPerBlock = Mathf.FloorToInt(maxEnemiesOnCircle / 4);
                movementSettings = new EnemyMovementSettingsVO(formation, EnemyMovementTypeEnum.ZigZag, Mathf.PI / 2, 1);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, 0, enemiesPerBlock);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, enemiesPerBlock, enemiesPerBlock * 2);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, enemiesPerBlock * 2, enemiesPerBlock * 3);
                SpawnEnemyBlock(movementSettings, EnemyTypeEnum.Basic, enemiesPerBlock * 3, enemiesPerBlock * 4);
                break;
        }
        wavePending = false;
    }

    void SpawnScout()
    {
        EnemyMovementSettingsVO movementSettings = new EnemyMovementSettingsVO(EnemyFormationEnum.Circle, EnemyMovementTypeEnum.Circle, 0, 1, false);
        SpawnEnemyAt(LevelConstants.MIN_ENEMY_SPAWN_DISTANCE + 5 * EnemyConstants.ENEMY_ROW_DISTANCE, 0, EnemyTypeEnum.Scout, movementSettings);
    }

    void SpawnEnemyBlock(EnemyMovementSettingsVO movementSettings, EnemyTypeEnum enemyType, float startCirclePos, float endCirclePos, int startRow = 0, int endRow = EnemyConstants.MAX_ENEMY_ROWS)
    {
        float circlePosRadians = Mathf.PI * 2 / EnemyConstants.MAX_ENEMIES_ON_CIRCLE;
        float startRadians = startCirclePos * circlePosRadians;
        float endRadians = endCirclePos * circlePosRadians;
        float startDistance = LevelConstants.MIN_ENEMY_SPAWN_DISTANCE + startRow * EnemyConstants.ENEMY_ROW_DISTANCE;
        float endDistance = LevelConstants.MIN_ENEMY_SPAWN_DISTANCE + endRow * EnemyConstants.ENEMY_ROW_DISTANCE;

        for (float angle = startRadians; angle < (endRadians - circlePosRadians * 0.5f); angle += circlePosRadians)
        {
            for (float distance = startDistance; distance < (endDistance - EnemyConstants.ENEMY_ROW_DISTANCE * 0.5); distance += EnemyConstants.ENEMY_ROW_DISTANCE)
            {
                SpawnEnemyAt(distance, angle, enemyType, movementSettings);
            }
        }
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
