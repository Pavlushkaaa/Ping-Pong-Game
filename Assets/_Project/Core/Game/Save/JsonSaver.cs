using UnityEngine;
using System.IO;

namespace Core
{
    public static class JsonSaver<T> where T : class
    {
        public static void Save(T saveTarget, string path)
        {
            string jsonString = JsonUtility.ToJson(saveTarget);

            File.WriteAllText(path, jsonString);
        }
        public static bool Load(string path, out T result)
        {
            if (File.Exists(path))
            {
                try
                {
                    result = JsonUtility.FromJson<T>(File.ReadAllText(path));
                    return true;
                }
                catch (IOException e) { Debug.Log(e.Message); }
            }

            result = null;
            return false;
        }
    }
}
