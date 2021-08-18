using UnityEngine;
using System.IO;
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
