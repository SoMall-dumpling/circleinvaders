using UnityEngine;

public class EnemyProperties : MonoBehaviour {

    public EnemyTypeEnum EnemyType = EnemyTypeEnum.Basic;
    public int HitsToKill = 1;
    public int ShootingRate = 0; // 0-100
    public int MovementSpeed = 1;
    public bool IsApproaching = true;

    public int CurrentHits = 0;

}
