using UnityEngine;
using System.IO;
using System.Runtime.Serialization;  
using System.Runtime.Serialization.Formatters.Binary;
using System;
public static class FileHandler
{
    /*
        Variables
    */

    //---------------------------------------------------
    /*
        Public Methods
    */
    public static bool CreateFile(string toWrite, string fileName)
    {
        if (string.IsNullOrEmpty(toWrite)) return false;
        try
        {
            using (StreamWriter file = new StreamWriter(FilePath(fileName)))
            {
                file.Write(toWrite);
                file.Close();
            }
            Debug.Log($"Path to file: {FilePath(fileName)}");
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool SerializeData<T>(T toSerialize)
    {
        try
        {
            Stream s = File.Open(FilePath("test.dat"), FileMode.Create);
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
    public static void SerializeData(string toSerialize)
    {
        try
        {
            Stream s = File.Open(FilePath("test.dat"), FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, toSerialize);
            s.Close();
        }
        catch (Exception exception)
        {
            Debug.LogError($"{exception.Message}");
        }
    }
    public static string DeSerializeData()
    {
        try
        {
            Stream s = File.Open(FilePath("test.dat"), FileMode.Open);
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
    public static string ReadFile(string fileName)
    {
        try
        {
            using (StreamReader streamReader = new StreamReader(FilePath(fileName)))
            {
                var result = streamReader.ReadToEnd();
                streamReader.Close();
                return result;
            }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
            return null;
        }
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
