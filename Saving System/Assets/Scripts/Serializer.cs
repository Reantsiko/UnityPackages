using System;
using UnityEngine;

[Serializable]
public class Serializer<T>
{
    /*
        Public Methods
    */
    public static T CreateFromJson(string response)
    {
        if (!string.IsNullOrEmpty(response))
            return JsonUtility.FromJson<T>(response);
        return default;
    }
    public static T CreateFromJsonRoot(string response, string root)
    {
        if (!string.IsNullOrEmpty(response) && !string.IsNullOrEmpty(root))
        {
            try
            {
                var temp = JsonUtility.FromJson<T>($"{{\"{root}\":{response}}}");
                return temp;
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error when trying to create with root \"{root}\" {typeof(T)}");
                Debug.LogError(exception.Message);
            }
        }
        return default;
    }
    public static void CreateFromJsonOverwrite(string response, T obj)
    {
        if (obj.Equals(default))
            return;
        if (!string.IsNullOrEmpty(response))
        {
            try
            {
                JsonUtility.FromJsonOverwrite(response, obj);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error when trying to overwrite object {typeof(T)}");
                Debug.LogError(exception);
            }
        }
    }
    public static string ToJson(T obj)
    {
        if (!obj.Equals(default))
            return JsonUtility.ToJson(obj);
        return null;
    }
}
