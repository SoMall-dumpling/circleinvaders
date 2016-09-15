public class EnemyMovementSettingsVO
{
    public EnemyFormationEnum Formation;
    public EnemyMovementTypeEnum MovementType;
    public float MaxAngle; // relative to original position
    public int StartDirection;

    public EnemyMovementSettingsVO(EnemyFormationEnum formation, EnemyMovementTypeEnum movementType, float maxAngle, int startDirection)
    {
        this.Formation = formation;
        this.MovementType = movementType;
        this.MaxAngle = maxAngle;
        this.StartDirection = startDirection;
    }

}
