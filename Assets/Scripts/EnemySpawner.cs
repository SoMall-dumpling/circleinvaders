using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject Enemy;

	void Start ()
    {
        SpawnEnemies();
	}
	
	void Update ()
    {
	
	}

    void SpawnEnemies()
    {
        float angleInDegrees = 0;
        Quaternion rotation = Quaternion.identity;
        for (float angle = 0; angle < Mathf.PI * 2; angle += Mathf.PI / 10)
        {
            if (angle % (Mathf.PI / 2) < 0.1) continue;
            for (float distance = 5.25f; distance > 3f; distance -= EnemyConstants.ENEMY_ROW_DISTANCE)
            {
                Vector3 position = new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);
                angleInDegrees = angle * (180 / Mathf.PI) - 90;
                rotation = Quaternion.Euler(0, 0, angleInDegrees);
                Instantiate(Enemy, position, rotation, transform);
            }
        }
    }
}
