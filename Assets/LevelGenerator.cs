using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<Transform> tile;
    public List<Transform> powerUps;
    public SpriteRenderer mapBounds;


    [Header("Map bounds")]
    public float minX=-3.4f;
    public float maxX = 3.6f;
    public float minY=0f;
    public float maxY=3.6f;

    public bool mirrorX;
    public float chanceToSpawnPowerUp;

    float xMargin;
    float yMargin;

    [Header("Rectangle")]
    int numberOfRectangles = 1;
    public int maxNumberOfRectangles = 8;
    public int rectangleWidth = 1;
    public int rectangleHeight = 1;

    private void Start()
    {
        //StartCoroutine(TEST());
        //GenerateTiles();
    }

    IEnumerator TEST()
    {
        for(int i=0;i<100;i++)
        {
            //GenerateTiles();
            yield return new WaitForSeconds(1f);
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            yield return null;
        }
        

    }
    void GenerateTilesV1()
    {
        for (float x = minX; x <= maxX; x += 0.4f)
        {
            for(float y=maxY; y>=minY; y-=0.2f)
            {
                int type = Random.Range(0, tile.Count);
                Instantiate(tile[type],new Vector3(x,y),Quaternion.identity,transform);
            }
        }
    }

    float floatError = 0.01f;
    
    Vector2[] GenerateOrigins()
    {
        Vector2[] origins = new Vector2[numberOfRectangles];
        for(int i=0;i<numberOfRectangles;i++)
        {
            bool valid = true;
            do
            {
                valid = true;
                origins[i] = new Vector2(RoundX(Random.Range(minX + xMargin, maxX - xMargin)), RoundY(Random.Range(minY + yMargin, maxY - yMargin)));
                if (i != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (origins[i] == origins[j])
                        {
                            valid = false;
                            //Debug.Log("Origin overlapping!!!");
                            break;
                        }
                    }

                }
            }
            while (!valid);
        }
        return origins;
    }

    public void GenerateTiles(int level)
    {
        numberOfRectangles = level+1;
        if(numberOfRectangles>maxNumberOfRectangles)
        {
            numberOfRectangles = maxNumberOfRectangles;
        }

        maxX = mirrorX ? 0f : maxX;
        xMargin = 0.4f * rectangleWidth;
        yMargin = 0.2f * rectangleHeight;

        Vector2[] origins = GenerateOrigins();
        for (int i=0;i<numberOfRectangles;i++)
        {
            int type = Random.Range(0, tile.Count);
            Vector2 origin = origins[i];
            if(IsPlaceFree(origin))
            {
                Instantiate(tile[type], origin, Quaternion.identity, transform);
            }
            for(float x=origin.x-0.4f*rectangleWidth;x<= origin.x + 0.4f * rectangleWidth+floatError;x+=0.4f)
            {
                for(float y=origin.y-0.2f*rectangleHeight;y<=origin.y+0.2f*rectangleHeight+floatError; y+=0.2f)
                {
                    Vector2 point = new Vector2(x, y);
                    if(IsPlaceOnMap(point) && IsPlaceFree(point))
                    {
                        Instantiate(tile[type], point, Quaternion.identity, transform);
                        
                    }
                }
            }
        }
        if(mirrorX)
        {
            MirrorTiles();
        }

        int powerupsAmount = (int)(chanceToSpawnPowerUp * transform.childCount);
        Debug.Log(powerupsAmount);
        GeneratePowerUps(powerupsAmount);
    }

    void MirrorTiles()
    {
        List<GameObject> tiles=new List<GameObject>();
        foreach(Transform children in transform)
        {
            tiles.Add(children.gameObject);
        }

        Vector3 flipX = new Vector3(-1f, 1f, 1f);
        foreach(GameObject tile in tiles)
        {
            Transform copiedTile = Instantiate(tile,tile.transform.position,Quaternion.identity,transform).transform;
            copiedTile.position = Vector3.Scale(copiedTile.position, flipX);
        }
    }

    public void ClearMap()
    {
        foreach(Transform children in transform)
        {
            Destroy(children.gameObject);
        }
    }

    public IEnumerator RegenerateMap(int level)
    {
        ClearMap();
        yield return null;
        GenerateTiles(level);
    }
    public void LoadTile(SerializableTile serializableTile)
    {
        Transform tileToLoad;
        foreach(Transform t in tile)
        {
            if(t.GetComponent<Tile>().type==serializableTile.type)
            {
                tileToLoad = t;
                Vector3 position=new Vector3(serializableTile.position[0],serializableTile.position[1],serializableTile.position[2]);

                Tile loadedTile = Instantiate(tileToLoad, position, Quaternion.identity, transform).GetComponent<Tile>();
                loadedTile.tileLife = serializableTile.life;
                loadedTile.powerUp = serializableTile.powerUp;
            }
        }

    }
    void GeneratePowerUps(int amount)
    {
        int powerUpTypes = System.Enum.GetValues(typeof(PowerUp)).Length;
        for (int i=0;i<amount;i++)
        {
            int tileNumber = Random.Range(1, transform.childCount);
            PowerUp randomPowerUp = (PowerUp)Random.Range(0, powerUpTypes);

            if(transform.GetChild(tileNumber).GetComponent<Tile>().powerUp==PowerUp.None)
            {
                transform.GetChild(tileNumber).GetComponent<Tile>().powerUp = randomPowerUp;
            }
            else
            {
                i--;
            }
        }
        
        /*
        foreach(Transform children in transform)
        {
            children.GetComponent<Tile>().powerUp = PowerUp.StickyPaddle;
        }
        */
        
    }
    float RoundX(float value)
    {
        return Mathf.Round(value * 2.5f) / 2.5f-0.2f;
    }
    float RoundY(float value)
    {
        return Mathf.Round(value * 5f) / 5f;
    }

    bool IsPlaceFree(Vector2 place)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(place, Vector2.one / 10f, 0f);
        return colliders.Length == 0;
    }

    bool IsPlaceOnMap(Vector2 place)
    {
        return (place.x >= minX && place.x <= maxX) && (place.y >= minY && place.y <= maxY);
    }
}
