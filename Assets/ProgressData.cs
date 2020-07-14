using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressData
{
    public List<SerializableTile> serializableTiles;
    public int health;
    public int points;
    public int highscore;
    public bool gameover;
    public int level;

    public ProgressData(List<SerializableTile> _serializableTiles, int _health, int _points, int _highscore, bool _gameover, int _level)
    {
        serializableTiles = new List<SerializableTile>();
        serializableTiles = _serializableTiles;

        health = _health;
        points = _points;
        highscore = _highscore;
        gameover = _gameover;
        level = _level;
    }
}
