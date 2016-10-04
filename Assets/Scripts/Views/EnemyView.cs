using UnityEngine;
using System.Collections;

public class EnemyView : MonoBehaviour {

    // TODO load Sprites dynamically

    public Sprite SpriteBasicEnemy;
    public Sprite SpriteBigEnemy;
    public Sprite SpriteSmallEnemy;
    public Sprite SpriteShooterEnemy;
    public Sprite SpriteScoutEnemy;

    private EnemyProperties enemyProperties;

    void Start()
    {
        enemyProperties = GetComponent<EnemyProperties>();
        SetSprite();
    }

    void SetSprite()
    {
        Sprite sprite = SpriteBasicEnemy;
        switch (enemyProperties.enemyType)
        {
            case EnemyTypeEnum.Big:
                sprite = SpriteBigEnemy;
                break;
            case EnemyTypeEnum.Small:
                sprite = SpriteSmallEnemy;
                break;
            case EnemyTypeEnum.Shooter:
                sprite = SpriteShooterEnemy;
                break;
            case EnemyTypeEnum.Scout:
                sprite = SpriteScoutEnemy;
                break;
        }
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
