using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
   private static readonly string PathLevels = Application.persistentDataPath + "/level.dat";
   //private static readonly string PathSystemData = Application.persistentDataPath + "/system.dat";

   public static void SaveLevels(MainMenu mm) {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(PathLevels, FileMode.Create);
      LevelData data = new LevelData(mm);
      
      formatter.Serialize(stream, data);
      stream.Close();
   }

   public static void SaveLevels(int score) {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(PathLevels, FileMode.Create);
      LevelData data = new LevelData(score);
      
      formatter.Serialize(stream, data);
      stream.Close();
   }

   public static LevelData LoadLevels() {
      
      if (File.Exists(PathLevels)) {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(PathLevels, FileMode.Open);

         LevelData data = formatter.Deserialize(stream) as LevelData;
         stream.Close();

         return data;
      }
      else {
         Debug.LogError($"Save file not found in: {PathLevels}");
         return null;
      }
   }

   /*
   public static void SaveSystemData(MainMenu mm) {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(PathSystemData, FileMode.Create);
      SystemData data = new SystemData(mm);
      
      formatter.Serialize(stream, data);
      stream.Close();
   }
   public static SystemData LoadSystemData() {

      if (File.Exists(PathSystemData)) {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(PathSystemData, FileMode.Open);

         SystemData data = formatter.Deserialize(stream) as SystemData; 
         stream.Close();

         return data;
      }
      else {
         Debug.LogError($"Save file not found in: {PathSystemData}");
         return null;
      }
   }
   */
}
