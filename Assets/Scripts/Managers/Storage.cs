using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Storage
{
    public static void SaveData(Player player, int level)
    {
        string dir = Application.persistentDataPath + Constants.DIRECTORY;
        if ( !Directory.Exists (dir) )
        {
            Directory.CreateDirectory (dir);
        }

        BinaryFormatter formatter = new BinaryFormatter ();
        string path = dir + Constants.DATA_NAME + level.ToString () + Constants.DATA_SUFFIX;
        FileStream stream = new FileStream (path, FileMode.Create);
        GameData data = new GameData (player, level);

        formatter.Serialize (stream, data);
        Debug.Log ("Saved " + +data.skillPoints);
        stream.Close ();


    }

    public static void LoadData( Player player, int level)
    {
        string dir = Application.persistentDataPath + Constants.DIRECTORY;
        string path = dir + Constants.DATA_NAME + level.ToString() + Constants.DATA_SUFFIX;
        if ( !Directory.Exists (dir) )
        {
            Directory.CreateDirectory (dir);
        }
        //all saved data will be lost
        DeleteAllOtherFiles (level);

        // create file with default data for 0 level
        if ( !File.Exists (path) )
        {
            Debug.Log ("file not exist");
            SaveData (player, 0); 
        }

        GameData data = new GameData (player, level);
        data = GetData (path);
        player.LoadGameData (data);

    }

    public static void DeleteAllOtherFiles(int level)
    {
        string dirPath = Application.persistentDataPath + Constants.DIRECTORY;
        string [] filesPathes = Directory.GetFiles(dirPath);
        for ( int i = 0; i < filesPathes.Length; i++ )
        {
            if ( i <= level ) continue;
            string path = dirPath + Constants.DATA_NAME + i.ToString () + Constants.DATA_SUFFIX;
            if ( File.Exists (path) )
                File.Delete (path);
        }
    }
   

    public static GameData GetData( string path )
    {
        BinaryFormatter formatter = new BinaryFormatter ();
        FileStream stream = new FileStream (path, FileMode.Open);

        GameData data = formatter.Deserialize (stream) as GameData;
        Debug.Log ("Loaded " + data.skillPoints);
        stream.Close ();
        return data;
    }

    public static bool IsStageAvaliable(int stageNumber)
    {
        string path = Application.persistentDataPath + Constants.DIRECTORY + Constants.DATA_NAME + stageNumber.ToString () + Constants.DATA_SUFFIX;
        return File.Exists (path);
    }


}
