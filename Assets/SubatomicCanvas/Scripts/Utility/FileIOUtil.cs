using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace SubatomicCanvas.Utility
{
    public class FileIOUtil
    {
        public static bool CouldSaveTitle(string title)
        {
            // 空文字列なら false を返す
            if (title == "") return false;

            // ファイル名に使用不可能な文字があれば false を返す
            var invalidChars = Path.GetInvalidFileNameChars();
            if (title.Any(c => invalidChars.Contains(c))) return false;
            
            return true;
        }
        
        public static bool DeleteJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError("File not found: " + filePath);
                return false;
            }

            try
            {
                File.Delete(filePath);
                Debug.Log("File deleted: " + filePath);
                return true;
            }
            catch (IOException ex)
            {
                Debug.LogError("Failed to delete file: " + filePath + "\n" + ex.Message);
                return false;
            }
        }

        public static IEnumerable<string> GetFileList(string searchPattern)
        {
            var directoryPath = Path.Combine(Application.dataPath, "SceneData");
            if (!Directory.Exists(directoryPath))
            {
                Debug.LogError("Directory not found: " + directoryPath);
                return null;
            }

            var files = Directory.GetFiles(directoryPath, searchPattern);
            return files;
        }

        public static string FormatToJsonData<T>(T data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        public static T ReadSceneData<T>(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public static object GetNewSaveFilePath(string fileName)
        {
            var directoryPath = Path.Combine(Application.dataPath, "SceneData");
            return Path.Combine(directoryPath, fileName);
        }
    }
}