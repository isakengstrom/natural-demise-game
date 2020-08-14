using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem  {
   
   public static void SaveLevels(MainMenu mm) {
      BinaryFormatter formatter = new BinaryFormatter();

      string path = Application.persistentDataPath + "/level.foo";
      FileStream stream = new FileStream(path, FileMode.Create);
      
      LevelData data = new LevelData(mm);
      
      formatter.Serialize(stream, data);
      stream.Close();
   }

   public static LevelData LoadLevels() {
      string path = Application.persistentDataPath + "/level.foo";
      if (File.Exists(path)) {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         LevelData data = formatter.Deserialize(stream) as LevelData;
         stream.Close();

         return data;
      }
      else {
         Debug.LogError("Save file not found in" + path);
         return null;
      }
   }
}
