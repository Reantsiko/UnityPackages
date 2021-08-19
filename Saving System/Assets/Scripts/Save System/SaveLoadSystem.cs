using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
[Serializable]
public static class SaveLoadSystem
{
    /*
        Variables
    */

    //---------------------------------------------------
    /*
        Public Methods
    */
    public static bool SerializeData<T>(T toSerialize, string fileName)
    {
        try
        {
            Stream s = File.Open(FilePath(fileName), FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, toSerialize);
            s.Close();
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogError($"{exception.Message}");
            return false;
        }
    }
    public static void SerializeMonoBehaviour<T>(T toSerialize, string fileName)
    {
        try
        {
            var json = Serializer<T>.ToJson(toSerialize);
            Stream s = File.Open(FilePath(fileName), FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, json);
            s.Close();
        }
        catch (Exception exception)
        {
            Debug.LogError($"{exception.Message}");
        }
    }
    public static string DeSerializeData(string fileName)
    {
        try
        {
            Stream s = File.Open(FilePath(fileName), FileMode.Open);
            BinaryFormatter b = new BinaryFormatter();
            var deSerialized = b.Deserialize(s).ToString();
            return deSerialized;
        }
        catch (Exception exception)
        {
            Debug.LogError($"{exception.Message}");
            return null;
        }
    }
    public static T DeSerializeData<T>()
    {
        try
        {
            Stream s = File.Open(FilePath("test.dat"), FileMode.Open);
            BinaryFormatter b = new BinaryFormatter();
            var deSerialized = (T)b.Deserialize(s);
            return deSerialized;
        }
        catch (Exception exception)
        {
            Debug.LogError($"{exception.Message}");
            return default;
        }
    }
    public static void DeSerializeOverwrite<T>(string fileName, T toOverwrite)
    {
        var response = DeSerializeData(fileName);
        Serializer<T>.CreateFromJsonOverwrite(response, toOverwrite);
    }
    //---------------------------------------------------
    /*
        Private Methods
    */
    private static string FilePath(string file)
    {
        if (string.IsNullOrEmpty(file)) return default;
        return $"{Application.persistentDataPath}/{file}";
    }
    //---------------------------------------------------
}
