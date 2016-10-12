using UnityEngine;

public class SpawnCovers : MonoBehaviour
{

    public GameObject CoverPiece;

    private GameObject[] covers;

    void Start()
    {
        SingletonMapper.Get<EventManager>().WaveStarted += OnWaveStarted;
    }

    void OnWaveStarted()
    {
        RemoveExistingCovers();
        SpawnNewCovers();
    }

    void RemoveExistingCovers()
    {
        if (covers == null) return;
        for (int i = 0; i < covers.Length; i++)
        {
            Destroy(covers[i]);
        }
    }

    void SpawnNewCovers()
    {
        int numCovers = GetNumCovers();
        int coverWidth = 7;
        int coverHeight = 3;

        covers = new GameObject[numCovers];
        for (int i = 0; i < numCovers; i++)
        {
            float angle = Mathf.PI * 2 / numCovers * i;
            float distance = 2.3f;
            Vector3 coverBasePosition = new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);
            GameObject c = SpawnCover(coverBasePosition, angle, coverWidth, coverHeight);
            covers[i] = c;
        }
    }

    GameObject SpawnCover(Vector3 basePosition, float angle, int w, int h)
    {
        GameObject container = new GameObject();
        container.name = "Cover";
        container.transform.position = basePosition;
        for (int row = 0; row < h; row++)
        {
            for (int col = 0; col < w; col++)
            {
                GameObject o = Instantiate(CoverPiece, container.transform, false) as GameObject;
                o.transform.localPosition = new Vector3((-w / 2 + col) * 0.16f, row * 0.16f, 0);
            }
        }
        container.transform.parent = gameObject.transform;
        container.transform.Rotate(0, 0, -90 + angle * 180 / Mathf.PI);
        return container;
    }

    private int GetNumCovers()
    {
        switch(SingletonMapper.Get<LevelStatsModel>().CurrentWaveNumber)
        {
            case 1:
            case 2:
                return 0;

            case 3:
            case 4:
                return 2;

            case 5:
            case 6:
            case 7:
            case 8:
                return 4;

            case 9:
            case 10:
                return 2;

            default:
                return 4;
        }
    }
}
