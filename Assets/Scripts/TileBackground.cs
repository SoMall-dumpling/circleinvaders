using UnityEngine;
using System.Collections;

public class TileBackground : MonoBehaviour {

    public GameObject Tile;

	void Start ()
    {
        CreateTiles();
	}

    void CreateTiles()
    {
        int startX = -10;
        int startY = -10;
        int maxX = 10;
        int maxY = 10;
        float tileW = Tile.GetComponent<Renderer>().bounds.max.x * 2;
        float tileH = Tile.GetComponent<Renderer>().bounds.max.y * 2;
        for (float x = startX; x < maxX; x += tileW) {
            for (float y = startY; y < maxY; y += tileH) { 
                Instantiate(Tile, new Vector3(x, y), Quaternion.identity, this.transform);
            }
        }
    }
}
