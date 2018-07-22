using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Saver {
	public static void Save(List<int> List,string FileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/"+ FileName + ".data");
        formatter.Serialize(file,List);
        file.Flush();
        file.Close();
    }
    public static List<int> load(string FileName)
    {
        if(!File.Exists(Application.persistentDataPath + "/" + FileName + ".data"))
        {
            return null;
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/"+ FileName + ".data", FileMode.Open);
            List<int> List = (List<int>)formatter.Deserialize(file);
            file.Close();
            return List;
        }
        
    }
    public static void Delete(string fileName)
    {
        File.Delete(Application.persistentDataPath + "/" + fileName + ".data");
    }
	}