using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<Transform> tile;
    public SpriteRenderer mapBounds;

    public float minX=-3.4f;
    public float maxX = 3.6f;
    public float minY=0f;
    public float maxY=3.6f;
    private void Start()
    {
        //GenerateTilesV1();
        //GenerateTilesV2();
        StartCoroutine(TEST());
    }

    IEnumerator TEST()
    {
        for(int i=0;i<100;i++)
        {
            GenerateTilesV2();
            yield return new WaitForSecondsRealtime(4f);
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
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
    void GenerateTilesV2()
    {
        for (int i=0;i<4;i++)
        {
            int type = Random.Range(0, tile.Count);
            Vector2 origin = new Vector2(RoundX(Random.Range(minX, maxX)), RoundY(Random.Range(minY, maxY)));
            //Debug.Log(origin);
            if(IsPlaceFree(origin))
            {
                Instantiate(tile[type], origin, Quaternion.identity, transform);
            }
            float squareWidth = 2f;
            for(float x=origin.x-0.4f*squareWidth;x<= origin.x + 0.4f * squareWidth+floatError;x+=0.4f)
            {
                for(float y=origin.y-0.2f*squareWidth;y<=origin.y+0.2f*squareWidth+floatError; y+=0.2f)
                {
                    Vector2 point = new Vector2(x, y);
                    if(IsPlaceOnMap(point))
                    {
                        bool condition = !IsPlaceFree(point);
                        Transform a= Instantiate(tile[type], point, Quaternion.identity, transform);
                        if (condition)
                        {
                            Destroy(a.gameObject);
                        }
                        
                    }
                }
            }
        }
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
        //return true;
        return (place.x >= minX && place.x <= maxX) && (place.y >= minY && place.y <= maxY);
    }
}
