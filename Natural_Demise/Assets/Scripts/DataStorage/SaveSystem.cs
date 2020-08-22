using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//A save system using binary formatting to store game data
//Following the tutorial made by Brackeys: https://www.youtube.com/watch?v=XOjd_qU2Ido
public static class SaveSystem {
   private static readonly string PathLevels = Application.persistentDataPath + "/level.dat";

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
}
