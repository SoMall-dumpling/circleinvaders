using UnityEngine;

public class CheatManager : MonoBehaviour
{

    public const bool IsCheatsEnabled = true;

    void Update()
    {
        if (IsCheatsEnabled && Input.GetKeyDown(KeyCode.Q))
        {
            KillEnemies();
        }
    }

    void KillEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TagConstants.ENEMY);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }

}
