using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableTile
{
    public float[] position;
    public float type;
    public PowerUp powerUp;
    public int life;

    public SerializableTile(Tile tile)
    {
        position = new float[3];

        position[0] = tile.transform.position.x;
        position[1] = tile.transform.position.y;
        position[2] = tile.transform.position.z;

        type = tile.type;
        powerUp = tile.powerUp;
        life = tile.tileLife;
    }
}
