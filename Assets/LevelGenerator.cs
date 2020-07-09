using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<Transform> tile;
    public SpriteRenderer mapBounds;
    private void Start()
    {
        GenerateTiles();
    }

    void GenerateTiles()
    {
        for (float x = -3.4f; x <= 3.6f; x += 0.4f)
        {
            for(float y=3.6f; y>=0f; y-=0.2f)
            {
                int type = Random.RandomRange(0, tile.Count);
                Instantiate(tile[type],new Vector3(x,y),Quaternion.identity,transform);
            }
        }
    }
}
