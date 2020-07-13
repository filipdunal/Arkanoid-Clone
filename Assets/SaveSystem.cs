using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveMap(List<Tile> tiles, int health, int points, int highscore, bool gameover)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedata.arkanoid";
        FileStream stream = new FileStream(path, FileMode.Create);

        List<SerializableTile> serializableTiles=new List<SerializableTile>();
        foreach(Tile tile in tiles)
        {
            serializableTiles.Add(new SerializableTile(tile));
        }
        ProgressData data = new ProgressData(serializableTiles,health,points,highscore,gameover);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ProgressData LoadData()
    {
        string path = Application.persistentDataPath + "/savedata.arkanoid";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ProgressData data = formatter.Deserialize(stream) as ProgressData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
