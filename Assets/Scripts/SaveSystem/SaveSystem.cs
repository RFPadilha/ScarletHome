using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem{
    
    public static void SavePlayer(PlayerScript player)
    {
        BinaryFormatter formatter = new BinaryFormatter();//formata os arquivos em binário
        string path = Application.persistentDataPath + "/player.vrau";//recebe onde o jogo está salvo e o nome do arquivo a ser salvo
        FileStream stream = new FileStream(path, FileMode.Create);//define a criação dos dados

        PlayerData data = new PlayerData(player);//recebe dados a serem salvos

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.vrau";
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        }else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
